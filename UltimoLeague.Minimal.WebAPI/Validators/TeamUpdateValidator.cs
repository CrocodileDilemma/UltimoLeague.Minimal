using FluentValidation;

namespace UltimoLeague.Minimal.WebAPI.Validators
{
    public class TeamUpdateValidator : Validator<TeamUpdateRequest>
    {
        public TeamUpdateValidator()
        {
            RuleFor(x => x.Code)
                .MaximumLength(20)
                .WithMessage("Team Code cannot be greater than 20 characters!");

            RuleFor(x => x.Name)
                .MaximumLength(100)
                .WithMessage("Team Name cannot be greater than 100 characters!");

            RuleFor(x => x.ContactFirstName)
                .MaximumLength(50)
                .WithMessage("Contact First Name cannot be greater than 50 characters!");

            RuleFor(x => x.ContactLastName)
                .MaximumLength(100)
                .WithMessage("Contact Last Name cannot be greater than 100 characters!");

            RuleFor(x => x.ContactEmail)
                .EmailAddress()
                .WithMessage("Email Address is not valid!")
                .MaximumLength(100)
                .WithMessage("Email Address cannot be greater than 100 characters!");

            RuleFor(x => x.ContactNumber)
               .MaximumLength(50)
               .WithMessage("Contact Number cannot be greater than 100 characters!");

            RuleFor(x => x.LeagueId)
                .Must(GlobalValidators.BeValidObjectId)
                .When(x => !string.IsNullOrEmpty(x.LeagueId))
                .WithMessage("League Id is not a valid Id!");


            RuleFor(x => x.SportId)
                .Must(GlobalValidators.BeValidObjectId)
                .When(x => !string.IsNullOrEmpty(x.SportId))
                .WithMessage("Sport Id is not a valid Id!");
        }
    }
}
