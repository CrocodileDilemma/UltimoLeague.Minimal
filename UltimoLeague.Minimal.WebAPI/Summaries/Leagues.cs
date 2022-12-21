using UltimoLeague.Minimal.WebAPI.Endpoints.Leagues;

namespace UltimoLeague.Minimal.WebAPI.Summaries.Leagues
{
    public class GetAllSummary : Summary<GetAll>
    {
        public GetAllSummary()
        {
            Summary = "Retrieve all Leagues";
            Description = "Retrieve a list of all League records.";
            // Response<IEnumerable<LeagueDto>>(200, "ok response with body");
        }
    }

    public class GetByIdSummary : Summary<GetById>
    {
        public GetByIdSummary()
        {
            Summary = "Retrieve a League by the Id";
            Description = "Retrieve a single League record by passing the Id.";
            // Response<LeagueDto>(200, "ok response with body");
        }
    }

    public class GetBySportIdSummary : Summary<GetBySportId>
    {
        public GetBySportIdSummary()
        {
            Summary = "Retrieve all Leagues by the Sport Id";
            Description = "Retrieve a list of League records by passing the Sport Id.";
            // Response<LeagueDto>(200, "ok response with body");
        }
    }

    public class PostSummary : Summary<Post>
    {
        public PostSummary()
        {
            Summary = "Create a new League";
            Description = "Create a new League record.";
            Response<LeagueDto>(200, "ok response with body");
        }
    }

    public class PutSummary : Summary<Put>
    {
        public PutSummary()
        {
            Summary = "Update an existing League";
            Description = "Update an existing League record.";
            Response<LeagueDto>(200, "ok response with body");
        }
    }
}
