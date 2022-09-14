using System.Runtime.Serialization;
using System.Text.Json.Serialization;

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

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Days
    {
        [EnumMember(Value = "Monday")]
        Monday = 1,
        [EnumMember(Value = "Tuesday")]
        Tuesday = 2,
        [EnumMember(Value = "Wednesday")]
        Wednesday = 3,
        [EnumMember(Value = "Thursday")]
        Thursday = 4,
        [EnumMember(Value = "Friday")]
        Friday = 5,
        [EnumMember(Value = "Saturday")]
        Saturday = 6,
        [EnumMember(Value = "Sunday")]
        Sunday = 7
    }

    public enum FixtureStatus
    {
        Scheduled = 0,
        Complete = 1,
    }
}
