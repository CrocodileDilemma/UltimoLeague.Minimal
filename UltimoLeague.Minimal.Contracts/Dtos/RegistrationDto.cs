using MongoDB.Bson.Serialization.Attributes;
using System.Runtime.Serialization;

namespace UltimoLeague.Minimal.Contracts.Dtos
{
    public class RegistrationDto : BaseDto
    {
        public string RegistrationNumber { get; set; }
        public DateTime RegistrationDate { get; set; }
        public PlayerBaseDto Player { get; set; }
        public TeamBaseDto Team { get; set; }
        public TeamBaseDto? PreviousTeam { get; set; }
    }
}
