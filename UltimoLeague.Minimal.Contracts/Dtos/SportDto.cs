namespace UltimoLeague.Minimal.Contracts.Dtos
{
    public class SportMinimalDto : BaseDto
    {
        public string SportName { get; set; }
        public string Description { get; set; }
    }
    public class SportDto : SportMinimalDto
    {
        public int Duration { get; set; }
        public int Leeway { get; set; }
        public int PointsForWin { get; set; }
        public int PointsForDraw { get; set; }
        public int PointsForLoss { get; set; }
        public int PointsForBye { get; set; }
        public int PointsForForfeit { get; set; }       
        public string Rules { get; set; }
    }
}
