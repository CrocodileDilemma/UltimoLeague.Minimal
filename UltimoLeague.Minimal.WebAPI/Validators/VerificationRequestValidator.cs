using FluentValidation;

namespace UltimoLeague.Minimal.WebAPI.Validators
{
    public class VerificationRequestValidator : Validator<VerificationRequest>
    {
        public VerificationRequestValidator()
        {
            RuleFor(x => x.VerificationToken)
                    .NotEmpty()
                    .WithMessage("Verification Token is required!");
        }
    }
}
