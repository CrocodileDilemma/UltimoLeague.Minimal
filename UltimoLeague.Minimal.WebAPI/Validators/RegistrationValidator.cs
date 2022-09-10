using FluentValidation;

namespace UltimoLeague.Minimal.WebAPI.Validators
{
    public class RegistrationValidator : Validator<RegistrationRequest>
    {
        public RegistrationValidator()
        {
            RuleFor(x => x.TeamId)
                .NotEmpty()
                .WithMessage("Team Id is required!")
                .Must(GlobalValidators.BeValidObjectId).WithMessage("Team Id is not a valid Id!");

            RuleFor(x => x.PlayerId)
                .NotEmpty()
                .WithMessage("Player Id is required!")
                .Must(GlobalValidators.BeValidObjectId).WithMessage("Player Id is not a valid Id!");
        }
    }
}
