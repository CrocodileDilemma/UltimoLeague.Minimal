using System.Text.Json.Serialization;
using UltimoLeague.Minimal.DAL.Common;

namespace UltimoLeague.Minimal.Contracts.Dtos
{
    public class FixtureDto : BaseDto
    {
        public TeamMinimalDto Team { get; set; }
        public TeamMinimalDto TeamOpposition { get; set; }
        public ArenaDto? Arena { get; set; }
        public string SeasonId { get; set; }
        public LeagueMinimalDto League { get; set; }
        public DateTime FixtureDateTime { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public FixtureStatus Status { get; set; }
        public bool Bye { get; set; }
    }
}
