using FluentValidation;

namespace UltimoLeague.Minimal.WebAPI.Validators
{
    public class MembershipNoRequestValidator : Validator<MembershipNoRequest>
    {
        public MembershipNoRequestValidator()
        {
            RuleFor(x => x.MembershipNo)
                .MinimumLength(15)
                .MaximumLength(15)
                .WithMessage("Membership Number is not valid!");
        }
    }
}
