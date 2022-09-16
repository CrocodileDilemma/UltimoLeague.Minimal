using System.Text.Json.Serialization;
using UltimoLeague.Minimal.DAL.Common;

namespace UltimoLeague.Minimal.Contracts.Dtos
{
    public class StatisticDto : BaseDto
    {
        public FixtureResultDto FixtureResult { get; set; }
        public PlayerMinimalDto Player { get; set; }
        public int Score { get; set; }
        public DateTime StatisticDateTime { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public StatisticEvent StatisticEvent { get; set; }
    }
}
