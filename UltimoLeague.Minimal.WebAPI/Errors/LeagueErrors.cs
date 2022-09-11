namespace UltimoLeague.Minimal.WebAPI.Errors
{
    public static class LeagueErrors
    {
        public static string InvalidSport(string id)
        {
            return $"Invalid Sport Id { id } for the specified League!";
        }
    }
}
