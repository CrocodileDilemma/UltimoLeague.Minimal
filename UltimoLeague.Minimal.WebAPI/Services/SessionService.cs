using FastEndpoints.Security;
using System.Security.Claims;
using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.DAL.Interfaces;
using UltimoLeague.Minimal.WebAPI.Errors;
using UltimoLeague.Minimal.WebAPI.Models;
using UltimoLeague.Minimal.WebAPI.Utilities;

namespace UltimoLeague.Minimal.WebAPI.Services
{
    public class SessionService : BaseService<User>
    {
        public SessionService(IMongoRepository<User> repository) : base(repository) { }

        public async Task<Result<SessionDto>> Logon(SessionRequest request)
        {
            User user = await Repository.FindOneAsync(x => x.EmailAddress == request.EmailAddress);
            
            if (user is null)
            {
                return Result.Fail<SessionDto>(BaseErrors.ObjectNotFound<User>());
            }

            var jwtToken = JWTBearer.CreateToken(
                signingKey: "BrandNewTokenSigningKey",
                expireAt: DateTime.UtcNow.AddDays(1),
                roles: new[] { user.AdminUser ? Roles.Admin : Roles.User },
                claims: new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.EmailAddress)
                });

            return Result.Ok(new SessionDto { EmailAddress = request.EmailAddress, Token = jwtToken  });
        }

        public async Task<Result<User>> Register(SessionRequest request)
        {
            User user = await Repository.FindOneAsync(x => x.EmailAddress == request.EmailAddress);
            
            if (user is not null)
            {
                return Result.Fail<User>(BaseErrors.ObjectExists<User>());
            }

            var info = Generators.GeneratePasswordHash(request.Password);

            user = new User
            {
                EmailAddress = request.EmailAddress,
                PasswordHash = info.hash,
                PasswordSalt = info.salt,
                VerificationToken = Generators.GenerateVerificationToken()
            };

            return await base.Post(user);
        }
    }
}
