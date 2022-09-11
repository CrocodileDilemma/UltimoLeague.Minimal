namespace UltimoLeague.Minimal.WebAPI.Errors
{
    public static class PlayerErrors
    {
        public static string PlayerInactive()
        {
            return $"Cannot Register Player. Player is Inactive!";
        }

        public static string PlayerAlreadyRegistered(string team)
        {
            return $"Cannot Register Player. Player is Already Registered for Team { team }!";
        }
    }
}
