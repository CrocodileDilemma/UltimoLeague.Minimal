using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.DAL.Interfaces;

namespace UltimoLeague.Minimal.WebAPI.Utilities
{
    public static class Queries
    {
        public static IQueryable<PlayerDto> PlayerQuery(IMongoRepository<Player> repository, 
            IMongoRepository<Team> teamRepository)
        {
            return (from p in repository.AsQueryable()
                    join t in teamRepository.AsQueryable()
                    on p.ActiveTeamId equals t.Id into pt
                    from subteam in pt.DefaultIfEmpty()
                    select new PlayerDto
                    {
                        Id = p.Id.ToString(),
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        Active = p.Active,
                        EmailAddress = p.EmailAddress,
                        ContactNumber = p.ContactNumber,
                        DateOfBirth = p.DateOfBirth,
                        Gender = p.Gender,
                        MembershipNumber = p.MembershipNumber,
                        ActiveTeam = subteam == null ? null :
                        new TeamBaseDto
                        {
                            Code = subteam.Code,
                            Id = subteam.Id.ToString()
                        }
                    });
        }
    }
}
