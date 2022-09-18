namespace UltimoLeague.Minimal.WebAPI.Errors
{
    public class RegistrationErrors
    {
        public static string RegistrationWithIdApproved(string id)
        {
            return $"Registration with {id} has already been Approved!";
        }
    }
}
