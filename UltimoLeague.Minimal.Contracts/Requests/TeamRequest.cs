namespace UltimoLeague.Minimal.Contracts.Requests
{
    public class TeamRequest
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactNumber { get; set; }
        public string LeagueId { get; set; }
        public string SportId { get; set; }
    }

    public class TeamUpdateRequest : TeamRequest
    {
        public string Id { get; set; }
    }
}
