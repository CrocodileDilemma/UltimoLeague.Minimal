using FluentValidation;

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
                .WithMessage("Opposition Score cannot be less than 0!");

            RuleFor(x => x.Result)
                .IsInEnum()
                .WithMessage("Result is not valid!");
        }
    }
}
