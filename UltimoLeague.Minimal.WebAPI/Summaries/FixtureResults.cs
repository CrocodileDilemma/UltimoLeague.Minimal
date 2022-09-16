using UltimoLeague.Minimal.WebAPI.Endpoints.FixtureResults;

namespace UltimoLeague.Minimal.WebAPI.Summaries.FixtureResults
{
    public class FixtureResults
    {
        public class GetByIdSummary : Summary<GetById>
        {
            public GetByIdSummary()
            {
                Summary = "Retrieve a Fixture Result by the Id";
                Description = "Retrieve a single Fixture Result record by passing the Id.";
                // Response<PlayerDto>(200, "ok response with body");
            }
        }

        public class GetByFixtureIdSummary : Summary<GetByFixtureId>
        {
            public GetByFixtureIdSummary()
            {
                Summary = "Retrieve all Fixture Results by the Fixture Id";
                Description = "Retrieve a list of Fixture Results records by passing the Fixture Id.";
                // Response<PlayerDto>(200, "ok response with body");
            }
        }

        public class GetByTeamIdSummary : Summary<GetByTeamId>
        {
            public GetByTeamIdSummary()
            {
                Summary = "Retrieve all Fixture Results by the Team Id";
                Description = "Retrieve a list of Fixture Results records by passing the Team Id.";
                // Response<PlayerDto>(200, "ok response with body");
            }
        }

        public class PostSummary : Summary<Post>
        {
            public PostSummary()
            {
                Summary = "Create a new Fixture Result";
                Description = "Create a new Fixture Result record.";
            }
        }
    }
}
