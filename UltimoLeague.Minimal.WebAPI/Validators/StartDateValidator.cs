using FluentValidation;

namespace UltimoLeague.Minimal.WebAPI.Validators
{
    public class StartDateValidator : Validator<StartDateRequest>
    {
        public StartDateValidator()
        {
            RuleFor(x => x.StartDate)
                .NotEmpty()
                .WithMessage("Date is required!");
        }
    }
}
