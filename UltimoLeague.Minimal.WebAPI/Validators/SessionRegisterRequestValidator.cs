using FluentValidation;

namespace UltimoLeague.Minimal.WebAPI.Validators
{
    public class SessionRegisterRequestValidator : Validator<SessionRegisterRequest>
    {
        public SessionRegisterRequestValidator()
        {
            RuleFor(x => x.EmailAddress)
                .NotEmpty()
                .WithMessage("Email Address is required!")
                .EmailAddress()
                .WithMessage("Email Address is not valid!")
                .MaximumLength(100)
                .WithMessage("Email Address cannot be greater than 100 characters!");

            RuleFor(x => x.Password)
               .NotEmpty()
               .WithMessage("Password is required!")
               .MinimumLength(8)
               .WithMessage("Password must be greater than 8 characters!")
               .MaximumLength(50)
               .WithMessage("Password cannot be greater than 100 characters!");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password)
                .WithMessage("Passwords do not match!");
        }
    }
}