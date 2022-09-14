using System.Text.Json.Serialization;
using UltimoLeague.Minimal.DAL.Common;

namespace UltimoLeague.Minimal.Contracts.Dtos
{
    public class PlayerMinimalDto : BaseDto
    {
        public string PlayerName { get; set; }
    }

    public class PlayerBaseDto : BaseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string ContactNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Gender Gender { get; set; } = Gender.None;
        public string MembershipNumber { get; set; }
        public bool Active { get; set; }
    }

    public class PlayerDto : PlayerBaseDto
    {
        public TeamMinimalDto? ActiveTeam { get; set; }
    }
}
