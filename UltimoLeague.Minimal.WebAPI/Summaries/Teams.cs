using UltimoLeague.Minimal.WebAPI.Endpoints.Teams;

namespace UltimoTeam.Minimal.WebAPI.Summaries
{
    public class GetAllSummary : Summary<GetAll>
    {
        public GetAllSummary()
        {
            Summary = "Retrieve all Teams";
            Description = "Retrieve a list of all Team records.";
            // Response<IEnumerable<ArenaDto>>(200, "ok response with body");
        }
    }
    public class GetByIdSummary : Summary<GetById>
    {
        public GetByIdSummary()
        {
            Summary = "Retrieve a Team by the Id";
            Description = "Retrieve a single Team record by passing the Id.";
            // Response<TeamDto>(200, "ok response with body");
        }
    }

    public class GetByLeagueIdSummary : Summary<GetByLeagueId>
    {
        public GetByLeagueIdSummary()
        {
            Summary = "Retrieve all Teams by the League Id";
            Description = "Retrieve a list of Team records by passing the League Id.";
            // Response<TeamDto>(200, "ok response with body");
        }
    }

    public class PostSummary : Summary<Post>
    {
        public PostSummary()
        {
            Summary = "Create a new Team";
            Description = "Create a new Team record.";
            //Response<TeamDto>(200, "ok response with body");
        }
    }

    public class PutSummary : Summary<Put>
    {
        public PutSummary()
        {
            Summary = "Update an existing Team";
            Description = "Update an existing Team record.";
            Response<TeamDto>(200, "ok response with body");
        }
    }
}
