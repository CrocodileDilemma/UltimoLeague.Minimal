namespace UltimoLeague.Minimal.Contracts.Requests
{
    public class SportRequest
    {
        public string SportName { get; set; }
        public int Duration { get; set; }
        public int Leeway { get; set; }
    }

    public class SportUpdateRequest
    {
        public string Id { get; set; }
        public string? SportName { get; set; }
        public int? Duration { get; set; }
        public int? Leeway { get; set; }
    }
}
