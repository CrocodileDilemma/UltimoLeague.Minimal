using FluentValidation;
using UltimoLeague.Minimal.DAL.Common;
using UltimoLeague.Minimal.WebAPI.Utilities;

namespace UltimoLeague.Minimal.WebAPI.Validators
{
    public class SeasonValidator : Validator<SeasonRequest>
    {
        public SeasonValidator()
        {
            RuleFor(x => x.StartDate)
                .NotEmpty()
                .WithMessage("Start Date is required!");

            RuleFor(x => x.NoOfMatches)
                .GreaterThan(0)
                .WithMessage("Number of matches must be a value greater than 0!");

            RuleFor(x => x.LeagueId)
                .NotEmpty()
                .WithMessage("League Id is required!")
                .Must(GlobalValidators.BeValidObjectId)
                .WithMessage("League Id is not a valid Id!");

            RuleFor(x => x.StartTime)
                .NotEmpty()
                .WithMessage("Start Time is required!")
                .Must(x => BeValidTime(x))
                .WithMessage("End Time is not a Valid Time!");

            RuleFor(x => x.EndTime)
                .NotEmpty()
                .WithMessage("End Time is required!")
                .Must(x => BeValidTime(x))
                .WithMessage("End Time is not a Valid Time!")
                .GreaterThan(x => x.StartTime)
                .WithMessage("End Time must be after Start Time!");

            RuleFor(x => x.MatchDays)
                .NotEmpty()
                .WithMessage("Match Days are required!")
                .Must((model, x) => StartOnStartDate(model.StartDate, x))
                .WithMessage("Season Start Date must be the first Match Day!")
                .Must(x => NotStartOnSunday(x))
                .WithMessage("Season cannot Start on a Sunday!");
                //.IsInEnum<Days>()
                //.WithMessage("Match Days must be valid Days!");

        }

        private bool BeValidTime(string time)
        {
            return TimeOnly.TryParse(time, out _);
        }

        private bool NotStartOnSunday(IEnumerable<Days> matchDays)
        {
            return !(matchDays.Count() == 1 && Generators.GetFirstMatchDay(matchDays.ToList()) == 0);
        }

        private bool StartOnStartDate(DateTime startDate, IEnumerable<Days> matchDays)
        {
            return (int)startDate.DayOfWeek == Generators.GetFirstMatchDay(matchDays.ToList());
        }
    }
}
