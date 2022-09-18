using FluentValidation;

namespace UltimoLeague.Minimal.WebAPI.Validators
{
    public class EmailAddressRequestValidator : Validator<EmailAddressRequest>
    {
        public EmailAddressRequestValidator()
        {
            RuleFor(x => x.EmailAddress)
                .EmailAddress()
                .WithMessage("Email Address is not valid!");
        }
    }
}
