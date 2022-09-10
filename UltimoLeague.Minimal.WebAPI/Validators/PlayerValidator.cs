using FluentValidation;

namespace UltimoLeague.Minimal.WebAPI.Validators
{
    public class PlayerValidator : Validator<PlayerRequest>
    {
        public PlayerValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("First Name is required!")
                .MaximumLength(50)
                .WithMessage("First Name cannot be greater than 50 characters!");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Last Name is required!")
                .MaximumLength(100)
                .WithMessage("Last Name cannot be greater than 100 characters!");

            RuleFor(x => x.EmailAddress)
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

            RuleFor(x => x.DateOfBirth)
               .NotEmpty()
               .WithMessage("Date Of Birth is required!")
               .GreaterThan(DateTime.Now.AddYears(-100))
               .LessThan(DateTime.Now.AddYears(-10))
               .WithMessage("Date Of Birth is not valid!");

            RuleFor(x => x.Gender)
                .IsInEnum()
                .WithMessage("Gender is not valid!");
        }
    }
}
