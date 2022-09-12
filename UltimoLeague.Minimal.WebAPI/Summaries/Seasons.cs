using UltimoLeague.Minimal.WebAPI.Endpoints.Seasons;

namespace UltimoLeague.Minimal.WebAPI.Summaries.Seasons
{
    public class GetAllSummary : Summary<GetAll>
    {
        public GetAllSummary()
        {
            Summary = "Retrieve all Seasons";
            Description = "Retrieve a list of all Season records.";
            // Response<IEnumerable<ArenaDto>>(200, "ok response with body");
        }
    }
    public class GetByIdSummary : Summary<GetById>
    {
        public GetByIdSummary()
        {
            Summary = "Retrieve a Season by the Id";
            Description = "Retrieve a single Season record by passing the Id.";
            // Response<SeasonDto>(200, "ok response with body");
        }
    }

    public class GetByLeagueIdSummary : Summary<GetByLeagueId>
    {
        public GetByLeagueIdSummary()
        {
            Summary = "Retrieve all Seasons by the League Id";
            Description = "Retrieve a list of Season records by passing the League Id.";
            // Response<SeasonDto>(200, "ok response with body");
        }
    }

    public class PostSummary : Summary<Post>
    {
        public PostSummary()
        {
            Summary = "Create a new Season";
            Description = "Create a new Season record.";
            //Response<SeasonDto>(200, "ok response with body");
        }
    }
}
