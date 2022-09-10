namespace UltimoLeague.Minimal.Contracts.Dtos
{
    public class SeasonBaseDto : BaseDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public LeagueBaseDto League { get; set; }
        public int NoOfMatches { get; set; }
    }

    public class SeasonDto : SeasonBaseDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public LeagueBaseDto League { get; set; }
        public int NoOfMatches { get; set; }
        public TeamBaseDto Winner { get; set; }
        public TeamBaseDto RunnerUp { get; set; }
        public PlayerBaseDto MVP { get; set; }
    }
}
