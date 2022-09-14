using Microsoft.AspNetCore.Routing.Constraints;
using MongoDB.Bson;
using MongoDB.Driver.Core.Misc;
using UltimoLeague.Minimal.Contracts.Dtos;
using UltimoLeague.Minimal.Contracts.Requests;
using UltimoLeague.Minimal.DAL.Entities;

namespace UltimoLeague.Minimal.WebAPI.Mapping
{
    public class MappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ArenaRequest, Arena>();

            config.NewConfig<(FixtureUpdateRequest, TeamMinimal, TeamMinimal, Arena), Fixture>()
                .Map(dest => dest, src => src.Item1)
                .IgnoreIf((src, dest) => src.Item1.FixtureDateTime == null, dest => dest.FixtureDateTime)
                .IgnoreIf((src, dest) => src.Item1.Status == null, dest => dest.Status)
                .Map(dest => dest.Team, src => src.Item2)
                .Map(dest => dest.TeamOpposition, src => src.Item3)
                .Map(dest => dest.Arena, src => src.Item4);

            config.NewConfig<Fixture, FixtureDto>()
                .Map(dest => dest, src => src)
                .Map(dest => dest.League, src => src.League.Adapt<LeagueMinimalDto>())
                .Map(dest => dest.Team, src => src.Team.Adapt<TeamMinimalDto>())
                .Map(dest => dest.TeamOpposition, src => src.TeamOpposition.Adapt<TeamMinimalDto>());

            config.NewConfig<League, LeagueMinimal>()
                .Map(dest => dest, src => src)
                .Map(dest => dest.BaseId, src => src.Id);

            config.NewConfig<League, LeagueDto>()
                .Map(dest => dest, src => src)
                .Map(dest => dest.Sport, src => src.Sport.Adapt<SportMinimalDto>());

            config.NewConfig<LeagueMinimal, LeagueMinimalDto>()
                .Map(dest => dest, src => src)
                .Map(dest => dest.Id, src => src.BaseId);

            config.NewConfig<(LeagueRequest, Sport), League>()
                .Map(dest => dest, src => src.Item1)
                .Map(dest => dest.Sport, src => src.Item2.Adapt<SportMinimal>());

            config.NewConfig<PlayerUpdateRequest, Player>()
                .IgnoreNullValues(true);

            config.NewConfig<Player, PlayerDto>()
                .Map(dest => dest, src => src)
                .Map(dest => dest.ActiveTeam, src => src.ActiveTeam.Adapt<TeamMinimalDto>());

            config.NewConfig<Player, PlayerMinimalDto>()
               .Map(dest => dest, src => src)
               .Map(dest => dest.PlayerName, src => $"{src.FirstName} {src.LastName}");

            config.NewConfig<Player, PlayerMinimal>()
              .Map(dest => dest.BaseId, src => src.Id)
              .Map(dest => dest.PlayerName, src => $"{src.FirstName} {src.LastName}");

            config.NewConfig<PlayerMinimal, PlayerMinimalDto>()
              .Map(dest => dest, src => src)
              .Map(dest => dest.Id, src => src.BaseId);

            config.NewConfig<(string, Player, Team), Registration>()
               .Map(dest => dest.RegistrationNumber, src => src.Item1)
               .Map(dest => dest.Player, src => src.Item2.Adapt<PlayerMinimal>())
               .Map(dest => dest.Team, src => src.Item3.Adapt<TeamMinimal>())
               .Map(dest => dest.PreviousTeam, src => src.Item2.ActiveTeam.Adapt<TeamMinimal>());

            config.NewConfig<Registration, RegistrationDto>()
              .Map(dest => dest, src => src)
              .Map(dest => dest.Player, src => src.Player.Adapt<PlayerMinimalDto>())
              .Map(dest => dest.Team, src => src.Team.Adapt<TeamMinimalDto>())
              .Map(dest => dest.PreviousTeam, src => src.PreviousTeam.Adapt<TeamMinimalDto>());

            config.NewConfig<(SeasonRequest, LeagueMinimal, ObjectId), Season>()
                .Map(dest => dest, src => src.Item1)
                .Map(dest => dest.League, src => src.Item2)
                .Map(dest => dest.Id, src => src.Item3);

            config.NewConfig<Season, SeasonBaseDto>()
                .Map(dest => dest, src => src)
                .Map(dest => dest.League, src => src.League.Adapt<LeagueMinimalDto>());

            config.NewConfig<Sport, SportMinimal>()
              .Map(dest => dest, src => src)
              .Map(dest => dest.BaseId, src => src.Id);

            config.NewConfig<SportMinimal, SportMinimalDto>()
              .Map(dest => dest, src => src)
              .Map(dest => dest.Id, src => src.BaseId);

            config.NewConfig<SportRequest, Sport>();

            config.NewConfig<SportUpdateRequest, Sport>()
                .IgnoreNullValues(true);

            config.NewConfig<(Team, League), TeamDto>()
               .IgnoreNullValues(true)
               .Map(dest => dest, src => src.Item1)
               .Map(dest => dest.League, src => src.Item2);

            config.NewConfig<Team, TeamDto>()
                .Map(dest => dest, src => src)
                .Map(dest => dest.League, src => src.League.Adapt<LeagueMinimalDto>());

            config.NewConfig<Team, TeamMinimal>()
                .Map(dest => dest, src => src)
                .Map(dest => dest.BaseId, src => src.Id);

            config.NewConfig<TeamMinimal, TeamMinimalDto>()
                .Map(dest => dest, src => src)
                .Map(dest => dest.Id, src => src.BaseId);


            config.NewConfig<(TeamRequest, League), Team>()
                .Map(dest => dest, src => src.Item1)
                .Map(dest => dest.League, src => src.Item2.Adapt<LeagueMinimal>())
                .Map(dest => dest.Sport, src => src.Item2.Sport);

            config.NewConfig<(TeamUpdateRequest, League), Team>()
                .IgnoreNullValues(true)
                .Map(dest => dest, src => src.Item1)
                .Map(dest => dest.League, src => src.Item2.Adapt<LeagueMinimal>())
                .Map(dest => dest.Sport, src => src.Item2.Sport);
        }
    }
}
