using FluentValidation;

namespace UltimoLeague.Minimal.WebAPI.Validators
{
    public class SportUpdateValidator : Validator<SportUpdateRequest>
    {
        public SportUpdateValidator()
        {
            RuleFor(x => x.SportName)
                .MaximumLength(50)
                .WithMessage("Sports Name cannot be greater than 50 characters!");

            RuleFor(x => x.Duration)
                .GreaterThan(0)
                .WithMessage("Sport must have a duration!");

            RuleFor(x => x.Leeway)
                .GreaterThan(0)
                .WithMessage("Sport must have a leeway!");
        }
    }
}
