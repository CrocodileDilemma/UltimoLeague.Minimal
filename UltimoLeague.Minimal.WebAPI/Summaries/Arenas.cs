using UltimoLeague.Minimal.WebAPI.Endpoints.Arenas;

namespace UltimoLeague.Minimal.WebAPI.Summaries
{
    public class GetAllSummary : Summary<GetAll>
    {
        public GetAllSummary()
        {
            Summary = "Retrieve all Arenas";
            Description = "Retrieve a list of all Arena records.";
            // Response<IEnumerable<ArenaDto>>(200, "ok response with body");
        }
    }

    public class GetByIdSummary : Summary<GetById>
    {
        public GetByIdSummary()
        {
            Summary = "Retrieve an Arena by the Id";
            Description = "Retrieve a single Arena record by passing the Id.";
            // Response<ArenaDto>(200, "ok response with body");
        }
    }

    public class GetByNameSummary : Summary<GetByName>
    {
        public GetByNameSummary()
        {
            Summary = "Retrieve an Arena by the Name";
            Description = "Retrieve a single Arena record by passing the Name.";
            // Response<ArenaDto>(200, "ok response with body");
        }
    }

    public class PostSummary : Summary<Post>
    {
        public PostSummary()
        {
            Summary = "Create a new Arena";
            Description = "Create a new Arena record.";
            Response<ArenaDto>(200, "ok response with body");
        }
    }

    public class PutSummary : Summary<Put>
    {
        public PutSummary()
        {
            Summary = "Update an existing Arena";
            Description = "Update an existing Arena record.";
            Response<ArenaDto>(200, "ok response with body");
        }
    }
}
