namespace UltimoLeague.Minimal.WebAPI.Errors
{
    public class SeasonErrors
    {
        public static string UnacceptableSeason(int teamFixtures, int fixturesPerWeek)
        {
            return @$"Season Parameters are invalid as they will result in more fixtures ({ teamFixtures })
                required than are available ({ fixturesPerWeek }!";
        }
    }
}
