using FluentValidation;

namespace UltimoLeague.Minimal.WebAPI.Validators
{
    public class FixtureUpdateValidator : Validator<FixtureUpdateRequest>
    {
        public FixtureUpdateValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Id is required!")
                .Must(GlobalValidators.BeValidObjectId).WithMessage("Id is not a valid Id!");

            RuleFor(x => x.TeamId)
                 .Must(GlobalValidators.BeValidObjectIdOrNull).WithMessage("Team Id is not a valid Id!");

            RuleFor(x => x.TeamOppositionId)
                .Must(GlobalValidators.BeValidObjectIdOrNull).WithMessage("Opposition Team Id is not a valid Id!");

            RuleFor(x => x.ArenaId)
                .Must(GlobalValidators.BeValidObjectIdOrNull).WithMessage("Arena Id is not a valid Id!");

            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage("Status is not valid!");
        }
    }
}
