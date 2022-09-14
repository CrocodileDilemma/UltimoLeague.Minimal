using System.Text.Json.Serialization;

namespace UltimoLeague.Minimal.Contracts.Dtos
{
    public class TeamMinimalDto : BaseDto
    {
        public string Code { get; set; }
    }

    public class TeamDto : TeamMinimalDto
    {
        public string Name { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactNumber { get; set; }
        public LeagueMinimalDto League { get; set; }
    }
}
