namespace UltimoLeague.Minimal.Contracts.Dtos
{
    public class UserDto : BaseDto
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }    
        public bool AdminUser { get; set; }
    }
}
