namespace UltimoLeague.Minimal.Contracts.Requests
{
    public class SportRequest
    {
        public string SportName { get; set; }
        public int Duration { get; set; }
        public int Leeway { get; set; }
        public int PointsForWin { get; set; }
        public int PointsForDraw { get; set; }
        public int PointsForLoss { get; set; }
        public int PointsForBye { get; set; }
        public int PointsForForfeit { get; set; }
        public string Description { get; set; }
        public string Rules { get; set; }
    }

    public class SportUpdateRequest
    {
        public string Id { get; set; }
        public string? SportName { get; set; }
        public int? Duration { get; set; }
        public int? Leeway { get; set; }
        public int? PointsForWin { get; set; }
        public int? PointsForDraw { get; set; }
        public int? PointsForLoss { get; set; }
        public int? PointsForBye { get; set; }
        public int? PointsForForfeit { get; set; }
        public string? Description { get; set; }
        public string? Rules { get; set; }
    }
}
