
using MongoDB.Bson.Serialization.Attributes;
using System.Runtime.Serialization;
using UltimoLeague.Minimal.DAL.Common;

namespace UltimoLeague.Minimal.Contracts.Dtos
{
    public class LeagueBaseDto : BaseDto
    {
        public string Code { get; set; }     
    }
    public class LeagueDto : LeagueBaseDto
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public Gender Gender { get; set; }
        public SportDto Sport { get; set; }
    }
}
