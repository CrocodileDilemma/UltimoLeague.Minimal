namespace UltimoLeague.Minimal.Contracts.Dtos
{
    public class SeasonBaseDto : BaseDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public LeagueMinimalDto League { get; set; }
        public int NoOfMatches { get; set; }
    }

    public class SeasonDto : SeasonBaseDto
    {
        public TeamMinimalDto? Winner { get; set; }
        public TeamMinimalDto? RunnerUp { get; set; }
        public PlayerMinimalDto? MVP { get; set; }
    }
}
