using System.Text.Json.Serialization;
using UltimoLeague.Minimal.DAL.Common;

namespace UltimoLeague.Minimal.Contracts.Requests
{
    public class StatisticRequest
    {
        public string FixtureResultId { get; set; }
        public string PlayerId { get; set; }
        public int Score { get; set; }
        public DateTime StatisticDateTime { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public StatisticEvent StatisticEvent { get; set; }
    }
}
