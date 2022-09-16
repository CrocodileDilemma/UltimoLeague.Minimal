using System.Text.Json.Serialization;
using UltimoLeague.Minimal.DAL.Common;

namespace UltimoLeague.Minimal.Contracts.Dtos
{
    public class FixtureResultDto : BaseDto
    {
        public string FixtureId { get; set; }
        public TeamMinimalDto Team { get; set; }
        public int Score { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public FixtureResultStatus Result { get; set; }
    }
}
