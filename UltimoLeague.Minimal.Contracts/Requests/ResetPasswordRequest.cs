namespace UltimoLeague.Minimal.Contracts.Requests
{

    public class ResetPasswordRequest
    {
        public string ResetToken { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
