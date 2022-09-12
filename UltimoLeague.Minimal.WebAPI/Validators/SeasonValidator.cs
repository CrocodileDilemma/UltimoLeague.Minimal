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

            RuleFor(x => x.EndDate)
                .NotEmpty()
                .WithMessage("End Date is required!")
                .GreaterThan(x => x.StartDate)
                .WithMessage("End Date must be after Start Date!");

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
                .WithMessage("Start Time is required!");

            RuleFor(x => x.EndTime)
                .NotEmpty()
                .WithMessage("End Time is required!")
                .GreaterThan(x => x.StartTime)
                .WithMessage("End Time must be after Start Time!");

            RuleFor(x => x.MatchDays)
                .NotEmpty()
                .WithMessage("Match Days are required!");
                ////.Must((model, field) => StartOnStartDate(model.StartDate, field))
                ////.WithMessage("Season Start Date must be the first Match Day!")
                ////.Must((field) => StartOnSunday(field))
                ////.WithMessage("Season cannot Start on a Sunday!");
            //.IsInEnum<Days>()
            //.WithMessage("Match Days must be valid Days!");

        }

        private bool StartOnSunday(List<Days> matchDays)
        {
            return !(matchDays.Count == 1 && Generators.GetFirstMatchDay(matchDays) == 0);
        }

        private bool StartOnStartDate(DateTime startDate, List<Days> matchDays)
        {
            return (int)startDate.DayOfWeek == Generators.GetFirstMatchDay(matchDays);
        }
    }
}
