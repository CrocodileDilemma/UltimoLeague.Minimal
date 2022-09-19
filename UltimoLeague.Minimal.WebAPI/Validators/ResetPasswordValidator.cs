using FluentValidation;

namespace UltimoLeague.Minimal.WebAPI.Validators
{
    public class ResetPasswordValidator : Validator<ResetPasswordRequest>
    {
        public ResetPasswordValidator()
        {
            RuleFor(x => x.ResetToken)
                .NotEmpty()
                .WithMessage("Reset Token is required!");

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
