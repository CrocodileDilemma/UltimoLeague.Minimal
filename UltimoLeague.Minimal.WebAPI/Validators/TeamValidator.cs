using FluentValidation;

namespace UltimoLeague.Minimal.WebAPI.Validators
{
    public class TeamValidator : Validator<TeamRequest>
    {
        public TeamValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty()
                .WithMessage("Team Code is required!")
                .MaximumLength(20)
                .WithMessage("Team Code cannot be greater than 20 characters!");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Team Name is required!")
                .MaximumLength(100)
                .WithMessage("Team Name cannot be greater than 100 characters!");

            RuleFor(x => x.ContactFirstName)
                .NotEmpty()
                .WithMessage("Contact First Name is required!")
                .MaximumLength(50)
                .WithMessage("Contact First Name cannot be greater than 50 characters!");

            RuleFor(x => x.ContactLastName)
                .NotEmpty()
                .WithMessage("Contact Last Name is required!")
                .MaximumLength(100)
                .WithMessage("Contact Last Name cannot be greater than 100 characters!");

            RuleFor(x => x.ContactEmail)
                .NotEmpty()
                .WithMessage("Email Address is required!")
                .EmailAddress()
                .WithMessage("Email Address is not valid!")
                .MaximumLength(100)
                .WithMessage("Email Address cannot be greater than 100 characters!");

            RuleFor(x => x.ContactNumber)
               .NotEmpty()
               .WithMessage("Contact Number is required!")
               .MaximumLength(50)
               .WithMessage("Contact Number cannot be greater than 100 characters!");

            RuleFor(x => x.LeagueId)
                .NotEmpty()
                .WithMessage("League Id is required!")
                .Must(GlobalValidators.BeValidObjectId).WithMessage("League Id is not a valid Id!");


            RuleFor(x => x.SportId)
                .NotEmpty()
                .WithMessage("Sport Id is required!")
                .Must(GlobalValidators.BeValidObjectId).WithMessage("Sport Id is not a valid Id!");
        }
    }
}
