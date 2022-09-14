using UltimoLeague.Minimal.DAL.Entities;

namespace UltimoLeague.Minimal.WebAPI.Models
{
    public class FixtureDetailMatrix
    {
        public Arena Arena { get; set; }
        public DateTime FixtureDate { get; set; }
        public TeamMinimal Team { get; set; }
    }
}
