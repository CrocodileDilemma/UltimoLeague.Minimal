using System.Globalization;
using System.Security.Cryptography;

namespace UltimoLeague.Minimal.WebAPI.Utilities
{
    public static class Generators
    {
        const string CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public static string RegistrationNumber()
        {
            return GetDigits(20);
        }

        public static string MembershipNumber()
        {
            return GetDigits(15);
        }

        private static string GetDigits(int length)
        {
            return new string(Enumerable.Repeat(CHARS, length)
                .Select(s => s[new Random().Next(s.Length)]).ToArray());
        }
    }
}
