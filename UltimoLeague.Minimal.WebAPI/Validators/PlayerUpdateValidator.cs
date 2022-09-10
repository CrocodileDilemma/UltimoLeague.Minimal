using FluentValidation;

namespace UltimoLeague.Minimal.WebAPI.Validators
{
    public class PlayerUpdateValidator : Validator<PlayerUpdateRequest>
    {
        public PlayerUpdateValidator()
        {
            RuleFor(x => x.FirstName)
                .MaximumLength(50)
                .WithMessage("First Name cannot be greater than 50 characters!");

            RuleFor(x => x.LastName)
                .MaximumLength(100)
                .WithMessage("Last Name cannot be greater than 100 characters!");

            RuleFor(x => x.EmailAddress)
                .EmailAddress()
                .WithMessage("Email Address is not valid!")
                .MaximumLength(100)
                .WithMessage("Email Address cannot be greater than 100 characters!");

            RuleFor(x => x.ContactNumber)
               .MaximumLength(50)
               .WithMessage("Contact Number cannot be greater than 100 characters!");

            RuleFor(x => x.DateOfBirth)
               .GreaterThan(DateTime.Now.AddYears(-100))
               .LessThan(DateTime.Now.AddYears(-10))
               .WithMessage("Date Of Birth is not valid!");

            RuleFor(x => x.Gender)
                .IsInEnum()
                .WithMessage("Gender is not valid!");
        }
    }
}