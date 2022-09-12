using System.Text.Json.Serialization;

namespace UltimoLeague.Minimal.Contracts.Dtos
{
    public class TeamBaseDto
    {
        [JsonPropertyName("id")]
        public string TeamId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Code { get; set; }
    }

    public class TeamDto : BaseDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactNumber { get; set; }
        public LeagueBaseDto League { get; set; }
    }
}
