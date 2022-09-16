namespace UltimoLeague.Minimal.WebAPI.Errors
{
    public class FixtureResultErrors
    {
        public static string FixtureTeamDoesNotMatch(string id)
        {
            return $"Team with Id { id } is not part of the current Fixture!";
        }

        public static string FixtureTeamMustBeSupplied()
        {
            return $"You must supply a valid Opposition Team Id!";
        }

        internal static string CannotSetByeStatus()
        {
            return $"You cannot set the Status as a Bye for a Fixture that is not a Bye Fixture!";
        }
    }
}
