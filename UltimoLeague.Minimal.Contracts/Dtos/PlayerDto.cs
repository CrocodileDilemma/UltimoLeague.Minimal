using System.Text.Json.Serialization;
using UltimoLeague.Minimal.DAL.Common;

namespace UltimoLeague.Minimal.Contracts.Dtos
{
    public class PlayerBaseDto
    {
        [JsonPropertyName("id")]
        public string PlayerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class PlayerMinimalDto : BaseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string ContactNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; } = Gender.None;
        public string MembershipNumber { get; set; }
        public bool Active { get; set; }
    }

    public class PlayerDto : PlayerMinimalDto
    {
        public TeamBaseDto? ActiveTeam { get; set; }
    }
}
