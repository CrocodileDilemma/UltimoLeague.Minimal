namespace UltimoLeague.Minimal.Contracts.Dtos
{
    public class RegistrationDto : BaseDto
    {
        public string RegistrationNumber { get; set; }
        public DateTime RegistrationDate { get; set; }
        public PlayerMinimalDto Player { get; set; }
        public TeamMinimalDto Team { get; set; }
        public TeamMinimalDto? PreviousTeam { get; set; }
        public bool Approved { get; set; }
    }
}
