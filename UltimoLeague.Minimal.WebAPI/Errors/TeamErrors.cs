namespace UltimoLeague.Minimal.WebAPI.Errors
{
    public class TeamErrors
    {
        public static string InvalidLeague(string id)
        {
            return $"Invalid League Id {id}. There are no teams for the specified League!";
        }
    }
}
