using UltimoLeague.Minimal.WebAPI.Endpoints.Users;

namespace UltimoLeague.Minimal.WebAPI.Summaries.Users
{
    public class ForgotPasswordSummary : Summary<ForgotPassword>
    {
        public ForgotPasswordSummary()
        {
            Summary = "Send a Forgotten Password request";
            Description = "Sends an email containing a link in order to reset a forgotten password.";
            // Response<IEnumerable<ArenaDto>>(200, "ok response with body");
        }
    }

    public class LogonSummary : Summary<Logon>
    {
        public LogonSummary()
        {
            Summary = "Logon with an Email Address and Password";
            Description = "Create a session by logging in with am Email Address and Password.";
            // Response<ArenaDto>(200, "ok response with body");
        }
    }

    public class RegisterSummary : Summary<Register>
    {
        public RegisterSummary()
        {
            Summary = "Register a new User";
            Description = "Create a new User record with an Email Address and Password.";
            // Response<ArenaDto>(200, "ok response with body");
        }
    }

    public class ResetSummary : Summary<Reset>
    {
        public ResetSummary()
        {
            Summary = "Reset a Password";
            Description = "Reset a Password.";
        }
    }

    public class VerifySummary : Summary<Verify>
    {
        public VerifySummary()
        {
            Summary = "Verify a new User";
            Description = "Verify a new User record.";
            // Response<ArenaDto>(200, "ok response with body");
        }
    }
}
