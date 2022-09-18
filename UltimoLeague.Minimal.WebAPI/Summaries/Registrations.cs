using UltimoLeague.Minimal.WebAPI.Endpoints.Registrations;

namespace UltimoLeague.Minimal.WebAPI.Summaries.Registrations
{
    public class GetByIdSummary : Summary<GetById>
    {
        public GetByIdSummary()
        {
            Summary = "Retrieve a Registration by the Id";
            Description = "Retrieve a single Registration record by passing the Id.";
            // Response<RegistrationDto>(200, "ok response with body");
        }
    }

    public class GetByPlayerIdSummary : Summary<GetByPlayerId>
    {
        public GetByPlayerIdSummary()
        {
            Summary = "Retrieve all Registrations by the Player Id";
            Description = "Retrieve a list of Registration records by passing the Player Id.";
            // Response<RegistrationDto>(200, "ok response with body");
        }
    }

    public class GetByTeamIdSummary : Summary<GetByTeamId>
    {
        public GetByTeamIdSummary()
        {
            Summary = "Retrieve all Registrations by the Team Id";
            Description = "Retrieve a list of Registration records by passing the Team Id.";
            // Response<RegistrationDto>(200, "ok response with body");
        }
    }


    public class PostSummary : Summary<Post>
    {
        public PostSummary()
        {
            Summary = "Create a new Registration";
            Description = "Create a new Registration record.";
            //Response<RegistrationDto>(200, "ok response with body");
        }
    }

    public class PutSummary : Summary<Put>
    {
        public PutSummary()
        {
            Summary = "Approve a Registration";
            Description = "Approve an existing Registration record.";
            //Response<RegistrationDto>(200, "ok response with body");
        }
    }
}
