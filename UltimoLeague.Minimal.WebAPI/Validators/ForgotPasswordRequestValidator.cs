using FluentValidation;

namespace UltimoLeague.Minimal.WebAPI.Validators
{
    public class ForgotPasswordRequestValidator : Validator<ForgotPasswordRequest>
    {
        public ForgotPasswordRequestValidator()
        {
            RuleFor(x => x.EmailAddress)
                .EmailAddress()
                .WithMessage("Email Address is not valid!");
        }
    }
}
