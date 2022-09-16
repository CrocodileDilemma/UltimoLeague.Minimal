using MongoDB.Bson;
using System.Collections.ObjectModel;
using UltimoLeague.Minimal.DAL.Common;
using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.DAL.Interfaces;
using UltimoLeague.Minimal.WebAPI.Errors;
using UltimoLeague.Minimal.WebAPI.Mapping;
using UltimoLeague.Minimal.WebAPI.Services.Interfaces;

namespace UltimoLeague.Minimal.WebAPI.Services
{
    public class FixtureResultService : IFixtureResultService
    {
        private IMongoRepository<FixtureResult> _repository;
        private IMongoRepository<Fixture> _fixtureRepository;
        public FixtureResultService(IMongoRepository<FixtureResult> repository, IMongoRepository<Fixture> fixtureRepository)
        {
            _repository = repository;
            _fixtureRepository = fixtureRepository;
        }

        public async Task<Result<IEnumerable<FixtureResultDto>>> Post(FixtureResultRequest request)
        {
            var fixture = await _fixtureRepository.FindOneAsync(x => x.Id == request.FixtureId.ToObjectId());            

            if (fixture is null)
            {
                return Result.Fail<IEnumerable<FixtureResultDto>>(BaseErrors.ObjectNotFoundWithId<Fixture>(request.FixtureId));
            }

            if (fixture.Team.BaseId != ObjectId.Empty && (!(fixture.Team.BaseId == request.TeamId.ToObjectId()
                || fixture.Team.BaseId == request.OppositionId?.ToObjectId())))
            {
                return Result.Fail<IEnumerable<FixtureResultDto>>(FixtureResultErrors.FixtureTeamDoesNotMatch(request.TeamId));
            }

            if (fixture.TeamOpposition.BaseId != ObjectId.Empty && (!(fixture.TeamOpposition.BaseId == request.TeamId.ToObjectId()
                || fixture.TeamOpposition.BaseId == request.OppositionId?.ToObjectId())))
            {
                if (string.IsNullOrEmpty(request.OppositionId))
                {
                    return Result.Fail<IEnumerable<FixtureResultDto>>(FixtureResultErrors.FixtureTeamMustBeSupplied());
                }

                return Result.Fail<IEnumerable<FixtureResultDto>>(FixtureResultErrors.FixtureTeamDoesNotMatch(request.OppositionId));
            }

            if (request.Result == FixtureResultStatus.Bye && !fixture.Bye)
            {
                return Result.Fail<IEnumerable<FixtureResultDto>>(FixtureResultErrors.CannotSetByeStatus());
            }

            ICollection<FixtureResult> results = new Collection<FixtureResult>();

            results.Add(new FixtureResult
            {
                FixtureId = request.FixtureId.ToObjectId(),
                Score = this.ValidateScore(request),
                Result = request.Result,
                Team = request.TeamId.ToObjectId() == fixture.Team.BaseId ? fixture.Team : fixture.TeamOpposition
            });

            if (!string.IsNullOrEmpty(request.OppositionId))
            {
                results.Add(new FixtureResult
                {
                    FixtureId = request.FixtureId.ToObjectId(),
                    Score = this.ValidateScore(request, true),
                    Result = this.ValidateResult(request),
                    Team = request.OppositionId.ToObjectId() == fixture.Team.BaseId ? fixture.Team : fixture.TeamOpposition
                });
            }

            try
            {
                await _repository.DeleteManyAsync(x => x.FixtureId == fixture.Id);

                if (results.Count() > 1)
                {
                    await _repository.InsertManyAsync(results);
                }
                else
                {
                    await _repository.InsertOneAsync(results.First());
                }

                fixture.Status = FixtureStatus.Complete;
                await _fixtureRepository.ReplaceOneAsync(fixture);

                return Result.Ok(results.AsEnumerable()
                    .Adapt<IEnumerable<FixtureResultDto>>());
            }
            catch (Exception ex)
            {
                return Result.Fail<IEnumerable<FixtureResultDto>>(BaseErrors.OperationFailed(ex));
            }
        }

        private FixtureResultStatus ValidateResult(FixtureResultRequest request)
        {
            switch (request.Result)
            {
                case FixtureResultStatus.Win:
                    return FixtureResultStatus.Loss;

                case FixtureResultStatus.Loss:
                    return FixtureResultStatus.Win;

                case FixtureResultStatus.ForfeitWin:
                    return FixtureResultStatus.ForfeitLoss;

                case FixtureResultStatus.ForfeitLoss:
                    return FixtureResultStatus.ForfeitWin;

                default:
                    return request.Result;
            }
        }
        private int ValidateScore(FixtureResultRequest request, bool opposition = false)
        {
            if (request.Result == FixtureResultStatus.ForfeitLoss || request.Result == FixtureResultStatus.ForfeitWin ||
                request.Result == FixtureResultStatus.Bye)
            {
                return 0;
            }

            if (request.Result == FixtureResultStatus.Draw || !opposition)
            {
                return request.Score;
            }

            return request.OppositionScore;
        }
    }
}
