using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using System.Security.Cryptography;
using UltimoLeague.Minimal.DAL.Common;
using UltimoLeague.Minimal.DAL.Entities;

namespace UltimoLeague.Minimal.WebAPI.Utilities
{
    public static class Generators
    {
        private const string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static Random _rng = new Random();

        public static TeamMinimal ByeFixture()
        {
            return new TeamMinimal { Code = "Bye" };
        }
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = _rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

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
            return new string(Enumerable.Repeat(_chars, length)
                .Select(s => s[_rng.Next(s.Length)]).ToArray());
        }

        internal static List<TimeOnly> GetFixturesPerDay(Sport sport, TimeOnly startTime, TimeOnly endTime)
        {
            int totalFixtureDuration = 90;// sport.Duration + sport.Leeway;
            List<TimeOnly> result = new();
            TimeOnly fixtureTime = startTime;

            while (fixtureTime <= endTime)
            {
                result.Add(fixtureTime);
                fixtureTime = fixtureTime.AddMinutes(totalFixtureDuration);
            }

            return result;
        }

        private static List<DateTime> InitializeFixtureDays(DateTime startDate, List<Days> matchDays)
        {
            List<DateTime> result = new();
            DateTime matchDay = startDate;

            foreach (int day in matchDays.OrderBy(day => day))
            {
                if (day != (int)matchDay.DayOfWeek)
                {
                    int dayOfWeek = day;
                    if (day == 7)
                    {
                        dayOfWeek = 0;
                    }

                    int daysUntil = (dayOfWeek - (int)matchDay.DayOfWeek + 7) % 7;
                    matchDay = matchDay.AddDays(daysUntil);
                }

                result.Add(matchDay);
            }

            return result;
        }

        internal static List<Fixture> GenerateFixtures(List<TeamMinimal> initialTeams, IQueryable<Arena> arenas,
            SeasonRequest request, List<TimeOnly> fixtureTimes, ObjectId seasonId, LeagueMinimal league)
        {
            List<Fixture> result = new List<Fixture>();

            List<Days> matchDays = new List<Days>();
            foreach (int day in request.MatchDays)
            {
                matchDays.Add((Days)day);
            }
            
            List<DateTime> fixtureDays = InitializeFixtureDays(request.StartDate, matchDays);

            var initialFixtureDetails = (from d in fixtureDays
                                              from t in fixtureTimes
                                              from a in arenas
                                              select new
                                              {
                                                  FixtureDay = d.Date,
                                                  FixtureTime = t,
                                                  Arena = a,
                                              }).ToList();


            for (int matchNo = 0; matchNo < request.NoOfMatches; matchNo++)
            {
                var teams = initialTeams.Select(x => new TeamMinimal
                {
                    BaseId = x.BaseId,
                    Code = x.Code
                }).ToList();

                while (result.Count < (teams.Count / 2) * (teams.Count - 1))
                {
                    var fixtureDetails = initialFixtureDetails.Select(x => new
                    {
                        FixtureDay = x.FixtureDay,
                        FixtureTime = x.FixtureTime,
                        Arena = x.Arena
                    }).ToList();

                    for (int i = 0; i < teams.Count; i += 2)
                    {
                        int index = _rng.Next(0, fixtureDetails.Count);
                        var fixtureDetail = fixtureDetails[index];

                        var teamOne = teams[i];
                        var teamTwo = teams[i + 1];
                        bool isBye = teamOne.BaseId == ObjectId.Empty || teamTwo.BaseId == ObjectId.Empty;

                        result.Add(new Fixture
                        {
                            Team = teamOne,
                            TeamOpposition = teamTwo,
                            Arena = isBye ? null : fixtureDetail.Arena,
                            FixtureDateTime = fixtureDetail.FixtureDay.Add(fixtureDetail.FixtureTime.ToTimeSpan()),
                            SeasonId = seasonId,
                            Status = FixtureStatus.Scheduled,
                            League = league,
                            Bye = isBye
                        });

                        if (!isBye)
                        {
                            fixtureDetails.RemoveAt(index);
                        }
                    }

                    //Rearrange the list
                    for (int i = teams.Count - 1; i > 1; i--)
                    {
                        TeamMinimal tempTeam = teams[i - 1];
                        teams[i - 1] = teams[i];
                        teams[i] = tempTeam;
                    }

                    initialFixtureDetails = initialFixtureDetails.Select(x => new
                    {
                        FixtureDay = x.FixtureDay.AddDays(7),
                        FixtureTime = x.FixtureTime,
                        Arena = x.Arena
                    }).ToList();
                }
            }

            return result.OrderBy(x => x.FixtureDateTime).ToList();
        }

        internal static int GetFirstMatchDay(List<Days> matchDays)
        {
            int firstMatchDay = matchDays.Select(x => (int)x).Min();
            if (firstMatchDay == 7)
            {
                firstMatchDay = 0;
            }

            return firstMatchDay;
        }

        internal static (byte[] salt, byte[] hash) GeneratePasswordHash(string password)
        {
            byte[] salt;
            byte[] hash;
            
            using (var hmac = new HMACSHA512())
            {
                salt = hmac.Key;
                hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

            return (salt, hash);
        }

        internal static string GenerateToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }

        internal static bool VerifyUserPasswordHash(User user, string password)
        {
            if (user == null)
            {
                return false;
            }
            
            using (var hmac = new HMACSHA512(user.PasswordSalt))
            {
                var hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return hash.SequenceEqual(user.PasswordHash);
            }
        }
    }
}
