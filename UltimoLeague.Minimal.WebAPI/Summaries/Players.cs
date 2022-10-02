using UltimoLeague.Minimal.WebAPI.Endpoints.Players;

namespace UltimoPlayer.Minimal.WebAPI.Summaries.Players
{
    public class GetByIdSummary : Summary<GetById>
    {
        public GetByIdSummary()
        {
            Summary = "Retrieve a Player by the Id";
            Description = "Retrieve a single Player record by passing the Id.";
            // Response<PlayerDto>(200, "ok response with body");
        }
    }

    public class GetByMembershipSummary : Summary<GetByMembership>
    {
        public GetByMembershipSummary()
        {
            Summary = "Retrieve a Player by the Membership No.";
            Description = "Retrieve a single Player record by passing the Membership Number.";
            // Response<PlayerDto>(200, "ok response with body");
        }
    }

    public class GetByEmailSummary : Summary<GetByEmail>
    {
        public GetByEmailSummary()
        {
            Summary = "Retrieve a Player by the Email Address";
            Description = "Retrieve a single Player record by passing the Email Address.";
            // Response<PlayerDto>(200, "ok response with body");
        }
    }

    public class GetByTeamIdSummary : Summary<GetByTeamId>
    {
        public GetByTeamIdSummary()
        {
            Summary = "Retrieve all Players by the Team Id";
            Description = "Retrieve a list of Player records by passing the Team Id.";
            // Response<PlayerDto>(200, "ok response with body");
        }
    }

    public class GetByUserIdSummary : Summary<GetByUserId>
    {
        public GetByUserIdSummary()
        {
            Summary = "Retrieve a Players by the User Id";
            Description = "Retrieve a single of Player record by passing the User Id.";
            // Response<PlayerDto>(200, "ok response with body");
        }
    }

    public class PostSummary : Summary<Post>
    {
        public PostSummary()
        {
            Summary = "Create a new Player";
            Description = "Create a new Player record.";
            //Response<PlayerDto>(200, "ok response with body");
        }
    }

    public class PutSummary : Summary<Put>
    {
        public PutSummary()
        {
            Summary = "Update an existing Player";
            Description = "Update an existing Player record.";
            Response<PlayerDto>(200, "ok response with body");
        }
    }
}
