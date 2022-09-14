using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using UltimoLeague.Minimal.Contracts.Dtos;
using UltimoLeague.Minimal.DAL.Common;

namespace UltimoLeague.Minimal.Contracts.Requests
{
    public class FixtureUpdateRequest
    {
        public string Id { get; set; }
        public string? TeamId { get; set; }
        public string? TeamOppositionId { get; set; }
        public string? ArenaId { get; set; }
        public DateTime? FixtureDateTime { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public FixtureStatus? Status { get; set; }
    }
}
