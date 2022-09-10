using FluentValidation;

namespace UltimoLeague.Minimal.WebAPI.Validators
{
    public class SeasonValidator : Validator<SeasonRequest>
    {
        public SeasonValidator()
        {
            RuleFor(x => x.StartDate)
                .NotEmpty()
                .WithMessage("Start Date is required!");

            RuleFor(x => x.EndDate)
                .NotEmpty()
                .WithMessage("End Date is required!")
                .GreaterThan(x => x.StartDate)
                .WithMessage("End Date must be after Start Date!");

            RuleFor(x => x.NoOfMatches)
                .GreaterThan(0)
                .WithMessage("Number of matches must be a value greater than 0!")
                .LessThan(x => (x.EndDate - x.StartDate).Days)
                .WithMessage("Number of Matches exceeds the number of days in the Season!");

            RuleFor(x => x.LeagueId)
                .NotEmpty()
                .WithMessage("League Id is required!")
                .Must(GlobalValidators.BeValidObjectId).WithMessage("League Id is not a valid Id!");
        }
    }
}
