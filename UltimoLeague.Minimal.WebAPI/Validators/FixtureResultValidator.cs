using FluentValidation;
using UltimoLeague.Minimal.DAL.Common;

namespace UltimoLeague.Minimal.WebAPI.Validators
{
    public class FixtureResultValidator : Validator<FixtureResultRequest>
    {
        public FixtureResultValidator()
        {
            RuleFor(x => x.FixtureId)
                .Must(GlobalValidators.BeValidObjectId)
                .WithMessage("Fixture Id is not a valid Id!");

            RuleFor(x => x.TeamId)
                .Must(GlobalValidators.BeValidObjectId)
                .WithMessage("Team Id is not a valid Id!");

            RuleFor(x => x.OppositionId)
                .Must(GlobalValidators.BeValidObjectIdOrNull)
                .WithMessage("Opposition Team Id is not a valid Id!")
                .NotEqual(x => x.TeamId)
                .WithMessage("Opposition Team Id cannot be the same as the Team Id!");

            RuleFor(x => x.Score)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Score cannot be less than 0!");

            RuleFor(x => x.OppositionScore)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Opposition Score cannot be less than 0!")
                .Must((model, x) => BeValidScore(model.Score, model.OppositionScore, model.Result))
                .WithMessage("Invalid Score, Opposition Score and Result!");

            RuleFor(x => x.Result)
                .IsInEnum()
                .WithMessage("Result is not valid!");
        }

        private bool BeValidScore(int score, int oppositionScore, FixtureResultStatus result)
        {
            if (score < oppositionScore && result == FixtureResultStatus.Win)
            {
                return false;
            }

            if (score > oppositionScore && result == FixtureResultStatus.Loss)
            {
                return false;
            }

            if (score != oppositionScore && result == FixtureResultStatus.Draw)
            {
                return false;
            }

            return true;
        }
    }
}
