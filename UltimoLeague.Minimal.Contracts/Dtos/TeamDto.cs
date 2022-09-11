namespace UltimoLeague.Minimal.Contracts.Dtos
{
    public class TeamBaseDto : BaseDto
    {
        public string Code { get; set; }
    }

    public class TeamDto : TeamBaseDto
    {
        public string Name { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactNumber { get; set; }
        public LeagueBaseDto League { get; set; }
    }
}
