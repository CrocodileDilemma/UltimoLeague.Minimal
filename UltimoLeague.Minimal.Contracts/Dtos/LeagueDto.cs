using System.Text.Json.Serialization;
using UltimoLeague.Minimal.DAL.Common;
using UltimoLeague.Minimal.DAL.Entities;

namespace UltimoLeague.Minimal.Contracts.Dtos
{
    public class LeagueMinimalDto : BaseDto
    {
        public string Code { get; set; }     
    }
    public class LeagueDto : LeagueMinimalDto
    {
        public string Name { get; set; }
        public int Level { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Gender Gender { get; set; }
        public SportMinimalDto Sport { get; set; }
    }
}
