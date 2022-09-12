namespace UltimoLeague.Minimal.DAL.Common
{
    public enum Gender
    {
        None = 0,
        Male = 1,
        Female = 2
    }

    public enum WinLossDraw
    {
        Nonoe = 0,
        Win = 1,
        Loss = 2,
        Draw = 3,
        Forfeit =43
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

}
