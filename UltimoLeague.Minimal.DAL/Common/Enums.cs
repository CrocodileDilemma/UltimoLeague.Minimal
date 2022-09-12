namespace UltimoLeague.Minimal.DAL.Common
{
    public enum Gender
    {
        None = 0,
        Male = 1,
        Female = 2
    }

    public enum FixtureResultStatus
    {
        None = 0,
        Win = 1,
        Loss = 2,
        Draw = 3,
        Forfeit = 4,
        Bye = 5
    }

    public enum StatisticEvent
    {
        None = 0,
        Goal = 1,
        RedCard = 2,
        YellowCard = 3,
        Foul = 4,
        SubstitutionOn = 5,
        SubstutionOff = 6,
        Assist = 7,
        Tackle = 8,
        Pass = 9
    }

    public enum Days
    {
        Monday = 0,
        Tuesday = 1,
        Wednesday = 2,
        Thursday = 3,
        Friday = 4,
        Saturday = 5,
        Sunday = 6
    }
    
    public enum FixtureStatus
    {
        Scheduled = 0,
        Completed = 1,
    }
}
