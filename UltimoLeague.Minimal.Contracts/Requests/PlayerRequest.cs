using UltimoLeague.Minimal.DAL.Common;

namespace UltimoLeague.Minimal.Contracts.Requests
{
    public class PlayerRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailAddress { get; set; }
        public string? ContactNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Gender Gender { get; set; } = Gender.None;
        public bool? Active { get; set; }
    }

    public class PlayerUpdateRequest : PlayerRequest
    {
        public string Id { get; set; }
    }
}
