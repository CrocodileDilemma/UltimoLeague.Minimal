﻿using MongoDB.Bson;
using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UltimoLeague.Minimal.Contracts.Requests;
using UltimoLeague.Minimal.DAL.Common;
using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.WebAPI.Mapping;

namespace UltimoLeague.Minimal.WebAPI.Utilities
{
    public static class Generators
    {
        private const string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static Random _rng = new Random();

        public static TeamBaseDto ByeFixture()
        {
            return new TeamBaseDto { TeamId = ObjectId.Empty.ToString(), Code = "Bye" };
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
            int totalFixtureDuration = sport.Duration + sport.Leeway;
            List<TimeOnly> result = new();
            TimeOnly fixtureTime = startTime;

            while (fixtureTime <= endTime)
            {
                result.Add(fixtureTime);
                fixtureTime.AddMinutes(totalFixtureDuration);
            }

            return result;
        }

        internal static List<Fixture> GenerateFixtures(List<TeamBaseDto> initialTeams,
            IQueryable<Arena> arenas, SeasonRequest request, List<TimeOnly> fixtureTimes, ObjectId seasonId)
        {
            List<Fixture> result = new List<Fixture>();
            List<DateTime> fixtureDays = InitializeFixtureDays(request.StartDate, request.MatchDays);

            var initialFixtureDetails = (from d in fixtureDays
                                              from t in fixtureTimes
                                              from a in arenas
                                              select new
                                              {
                                                  FixtureDay = d.Date,
                                                  FixtureTime = t,
                                                  ArenaId = a.Id,
                                              }).ToList();

          
            for (int matchNo = 0; matchNo < request.NoOfMatches; matchNo++)
            {
                var teams = initialTeams.Select(x => new TeamBaseDto
                {
                    TeamId = x.TeamId,
                    Code = x.Code
                }).ToList();

                while (result.Count < (teams.Count / 2) * (teams.Count - 1))
                {
                    var fixtureDetails = initialFixtureDetails.Select(x => new
                    {
                        FixtureDay = x.FixtureDay,
                        FixtureTime = x.FixtureTime,
                        ArenaId = x.ArenaId
                    }).ToList();


                    for (int i = 0; i < teams.Count; i += 2)
                    {
                        int index = _rng.Next(0, fixtureDetails.Count);
                        var fixtureDetail = fixtureDetails[index];

                        result.Add(new Fixture
                        {
                            TeamId = teams[i].TeamId.ToObjectId(),
                            TeamOppId = teams[i + 1].TeamId.ToObjectId(),
                            ArenaId = fixtureDetail.ArenaId,
                            FixtureDateTime = fixtureDetail.FixtureDay.Add(fixtureDetail.FixtureTime.ToTimeSpan()),
                            SeasonId = seasonId,
                            Status = FixtureStatus.Scheduled,
                            LeagueId = request.LeagueId.ToObjectId()
                        });

                        fixtureDetails.RemoveAt(index);
                    }

                    //Rearrange the list
                    for (int i = teams.Count - 1; i > 1; i--)
                    {
                        TeamBaseDto tempTeam = teams[i - 1];
                        teams[i - 1] = teams[i];
                        teams[i] = tempTeam;
                    }
                }
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

        internal static int GetFirstMatchDay(List<Days> matchDays)
        {
            int firstMatchDay = matchDays.Select(x => (int)x).Min();
            if (firstMatchDay == 7)
            {
                firstMatchDay = 0;
            }

            return firstMatchDay;
        }
    }
}
