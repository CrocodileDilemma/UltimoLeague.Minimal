using FluentValidation;

namespace UltimoLeague.Minimal.WebAPI.Validators
{
    public class LeagueValidator : Validator<LeagueRequest>
    {
        public LeagueValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty()
                .WithMessage("League Code is required!")
                .MaximumLength(10)
                .WithMessage("League Code cannot be greater than 10 characters!");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("League Name is required!")
                .MaximumLength(50)
                .WithMessage("League Name cannot be greater than 50 characters!");

            RuleFor(x => x.Gender)
                .IsInEnum()
                .WithMessage("Gender is not valid!");

            RuleFor(x => x.SportId)
                .NotEmpty()
                .WithMessage("Sport Id is required!")
                .Must(GlobalValidators.BeValidObjectId).WithMessage("Sport Id is not a valid Id!");         
        }
    }
}
