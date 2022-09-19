namespace UltimoLeague.Minimal.WebAPI.Errors
{
    public class UserErrors
    {
        public static string UserVerified()
        {
            return "User has already been Verified!";
        }

        internal static string InvalidUserNameOrPassword()
        {
            return "Invalid Username or Password!";
        }

        internal static string UserUnverified()
        {
            return "User has not been Verified!";
        }
    }
}
