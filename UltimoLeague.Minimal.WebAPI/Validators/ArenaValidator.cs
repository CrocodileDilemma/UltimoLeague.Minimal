using FluentValidation;

namespace UltimoLeague.Minimal.WebAPI.Validators
{
    public class ArenaValidator : Validator<ArenaRequest>
    {
        public ArenaValidator()
        {
            RuleFor(x => x.ArenaName)
                .NotEmpty()
                .WithMessage("Arena Name is required!")
                .MaximumLength(50)
                .WithMessage("Arena Name cannot be greater than 50 characters!!");
        }
    }
}
