using UltimoLeague.Minimal.WebAPI.Endpoints.Statistics;

namespace UltimoLeague.Minimal.WebAPI.Summaries.Statistics
{
    public class GetByIdSummary : Summary<GetById>
    {
        public GetByIdSummary()
        {
            Summary = "Retrieve a Statistic by the Id";
            Description = "Retrieve a single Statistic record by passing the Id.";
            // Response<SeasonDto>(200, "ok response with body");
        }
    }

    public class GetByFixtureIdSummary : Summary<GetByFixtureId>
    {
        public GetByFixtureIdSummary()
        {
            Summary = "Retrieve all Statistics by the Fixture Id";
            Description = "Retrieve a list of Statistic records by passing the Fixture Id.";
            // Response<SeasonDto>(200, "ok response with body");
        }
    }

    public class GetByPlayerIdSummary : Summary<GetByPlayerId>
    {
        public GetByPlayerIdSummary()
        {
            Summary = "Retrieve all Statistics by the Player Id";
            Description = "Retrieve a list of Statistic records by passing the Player Id.";
            // Response<SeasonDto>(200, "ok response with body");
        }
    }

    public class PostSummary : Summary<Post>
    {
        public PostSummary()
        {
            Summary = "Create a new Statistic";
            Description = "Create a new Statistic record.";
            //Response<SeasonDto>(200, "ok response with body");
        }
    }
}
