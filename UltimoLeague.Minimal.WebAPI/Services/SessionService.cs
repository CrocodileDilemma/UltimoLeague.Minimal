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

        public async Task<Result<string>> Register(SessionRequest request)
        {
            User user = await Repository.FindOneAsync(x => x.EmailAddress == request.EmailAddress);
            
            if (user is not null)
            {
                return Result.Fail<string>(BaseErrors.ObjectExists<User>());
            }

            var info = Generators.GeneratePasswordHash(request.Password);

            user = new User
            {
                EmailAddress = request.EmailAddress,
                PasswordHash = info.hash,
                PasswordSalt = info.salt,
                VerificationToken = Generators.GenerateVerificationToken()
            };

            var result = await base.Post(user);

            if (result.IsFailed)
            {
                return Result.Fail<string>(result.Errors);
            }

            return SendVerificationEmail(user);
        }

        public async Task<Result<string>> Verify(VerificationRequest request)
        {
            User user = await Repository.FindOneAsync(x => x.VerificationToken == request.VerificationToken);

            if (user is null)
            {
                return Result.Fail<string>(BaseErrors.ObjectExists<User>());
            }

            if (user.VerifiedAt is not null)
            {
                return Result.Fail<string>(UserErrors.UserVerified());
            }

            user.VerifiedAt = DateTime.Now;         
            
            var result = await base.Update(user);
            if (result.IsFailed)
            {
                return Result.Fail<string>(result.Errors);
            }

            return Result.Ok("Thank you for verifying. Please log in with your email and password");
        }

        private Result<string> SendVerificationEmail(User user)
        {
            return Result.Ok("Thank you, pLease follow the link in the email to verify your registration");
        }
    }
}
