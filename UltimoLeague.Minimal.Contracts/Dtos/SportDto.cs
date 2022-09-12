namespace UltimoLeague.Minimal.Contracts.Dtos
{
    public class SportDto : BaseDto
    {
        public string SportName { get; set; }
        public int Duration { get; set; }
        public int Leeway { get; set; }
    }
}
