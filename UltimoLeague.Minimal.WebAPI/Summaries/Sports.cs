using UltimoLeague.Minimal.WebAPI.Endpoints.Sports;

namespace UltimoLeague.Minimal.WebAPI.Summaries.Sports
{
    public class GetSummary : Summary<Get>
    {
        public GetSummary()
        {
            Summary = "Retrieve all Sport Names and Ids";
            Description = "Retrieve a minimal list of all Sport records (Name and Id).";
            // Response<IEnumerable<SportDto>>(200, "ok response with body");
        }
    }

    public class GetAllSummary : Summary<GetAll>
    {
        public GetAllSummary()
        {
            Summary = "Retrieve all Sports";
            Description = "Retrieve a list of all Sport records.";
            // Response<IEnumerable<SportDto>>(200, "ok response with body");
        }
    }

    public class GetByIdSummary : Summary<GetById>
    {
        public GetByIdSummary()
        {
            Summary = "Retrieve a Sport by the Id";
            Description = "Retrieve a single Sport record by passing the Id.";
            // Response<SportDto>(200, "ok response with body");
        }
    }

    public class GetByNameSummary : Summary<GetByName>
    {
        public GetByNameSummary()
        {
            Summary = "Retrieve a Sport by the Name";
            Description = "Retrieve a single Sport record by passing the Name.";
            // Response<SportDto>(200, "ok response with body");
        }
    }

    public class PostSummary : Summary<Post>
    {
        public PostSummary()
        {
            Summary = "Create a new Sport";
            Description = "Create a new Sport record.";
            Response<SportDto>(200, "ok response with body");
        }
    }

    public class PutSummary : Summary<Put>
    {
        public PutSummary()
        {
            Summary = "Update an existing Sport";
            Description = "Update an existing Sport record.";
            Response<PlayerDto>(200, "ok response with body");
        }
    }
}
