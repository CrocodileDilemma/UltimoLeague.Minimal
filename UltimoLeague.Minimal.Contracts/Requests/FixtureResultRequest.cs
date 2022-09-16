using System.Text.Json.Serialization;
using UltimoLeague.Minimal.DAL.Common;

namespace UltimoLeague.Minimal.Contracts.Requests
{
    public class FixtureResultRequest
    {
        public string FixtureId { get; set; }
        public string TeamId { get; set; }
        public int Score { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public FixtureResultStatus Result { get; set; }
        public string OppositionId { get; set; }
        public int OppositionScore { get; set; }
    }
}
