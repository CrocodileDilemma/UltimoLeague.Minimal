﻿using System.Text.Json.Serialization;
using UltimoLeague.Minimal.DAL.Common;

namespace UltimoLeague.Minimal.Contracts.Requests
{
    public class SeasonRequest
    {
        public DateTime StartDate { get; set; }
        public string LeagueId { get; set; }
        public int NoOfMatches { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public IEnumerable<Days> MatchDays { get; set; }
    }
}
