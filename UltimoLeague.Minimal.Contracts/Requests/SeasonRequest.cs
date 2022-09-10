namespace UltimoLeague.Minimal.Contracts.Requests
{
    public class SeasonRequest
    {
        public string Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string LeagueId { get; set; }
        public int NoOfMatches { get; set; }
    }
}
