using UltimoLeague.Minimal.DAL.Common;

namespace UltimoLeague.Minimal.Contracts.Dtos
{
    public class PlayerBaseDto : BaseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class PlayerMinimalDto : PlayerBaseDto
    {
        public string EmailAddress { get; set; }
        public string ContactNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; } = Gender.None;
        public string MembershipNumber { get; set; }
        public bool Active { get; set; }
    }

    public class PlayerDto : PlayerMinimalDto
    {
        public TeamBaseDto? ActiveTeam { get; set; }
    }
}
