using FastEndpoints;
using UltimoLeague.Minimal.WebAPI.Endpoints.Fixtures;

namespace UltimoLeague.Minimal.WebAPI.Summaries.Fixtures
{
    public class GetByIdSummary : Summary<GetById>
    {
        public GetByIdSummary()
        {
            Summary = "Retrieve a Fixture by the Id";
            Description = "Retrieve a single Fixture record by passing the Id.";
            // Response<PlayerDto>(200, "ok response with body");
        }
    }

    public class GetByDateSummary : Summary<GetByDate>
    {
        public GetByDateSummary()
        {
            Summary = "Retrieve all Fixtures by the Start Date";
            Description = "Retrieve a list of Fixture records by passing the Start Date.";
            // Response<PlayerDto>(200, "ok response with body");
        }
    }

    public class GetBySeasonIdSummary : Summary<GetBySeasonId>
    {
        public GetBySeasonIdSummary()
        {
            Summary = "Retrieve all Fixtures by the Season Id";
            Description = "Retrieve a list of Fixtures records by passing the Season Id.";
            // Response<PlayerDto>(200, "ok response with body");
        }
    }

    public class GetByTeamIdSummary : Summary<GetByTeamId>
    {
        public GetByTeamIdSummary()
        {
            Summary = "Retrieve all Fixtures by the Team Id";
            Description = "Retrieve a list of Fixtures records by passing the Team Id.";
            // Response<PlayerDto>(200, "ok response with body");
        }
    }

    public class PutSummary : Summary<Put>
    {
        public PutSummary()
        {
            Summary = "Update an existing Fixture";
            Description = "Update an existing Fixture record.";
            Response<PlayerDto>(200, "ok response with body");
        }
    }
}
