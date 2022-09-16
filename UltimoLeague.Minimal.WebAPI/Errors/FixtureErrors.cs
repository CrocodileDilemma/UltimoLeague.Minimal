namespace UltimoLeague.Minimal.WebAPI.Errors
{
    public static class FixtureErrors
    {
        public static string FixtureClash(DateTime startDate, string arenaName)
        {
            return $"New Fixture StartDate { startDate } and Arena {arenaName} will cause it to clash with another Fixture!";
        }
    }
}
