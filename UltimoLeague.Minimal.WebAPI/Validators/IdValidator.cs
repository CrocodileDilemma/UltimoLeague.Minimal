using FluentValidation;

namespace UltimoLeague.Minimal.WebAPI.Validators
{
    public class IdValidator : Validator<IdRequest>
    {
        public IdValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Id is required!")
                .Must(GlobalValidators.BeValidObjectId).WithMessage("Id is not a valid Id!");
        }
    }
}
