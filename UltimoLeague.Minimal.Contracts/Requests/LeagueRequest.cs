
using UltimoLeague.Minimal.DAL.Common;

namespace UltimoLeague.Minimal.Contracts.Requests
{
    public class LeagueRequest
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Level { get; set; } = 0;
        public Gender Gender { get; set; } = Gender.None;
        public string SportId { get; set; }
    }
}
