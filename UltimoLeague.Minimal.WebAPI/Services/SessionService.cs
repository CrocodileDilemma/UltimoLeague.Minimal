using DnsClient;
using FastEndpoints.Security;
using System.Runtime.InteropServices;
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
            
            if (!Generators.VerifyUserPasswordHash(user, request.Password))
            {
                return Result.Fail<SessionDto>(UserErrors.InvalidUserNameOrPassword());
            }

            if (user.VerifiedAt is null)
            {
                return Result.Fail<SessionDto>(UserErrors.UserUnverified());
            }

            DateTime expiry = DateTime.UtcNow.AddDays(1);

            var jwtToken = JWTBearer.CreateToken(
                signingKey: "BrandNewTokenSigningKey",
                expireAt: expiry,
                roles: new[] { user.AdminUser ? Roles.Admin : Roles.User },
                claims: new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.EmailAddress)
                });

            return Result.Ok(new SessionDto { EmailAddress = request.EmailAddress, Token = jwtToken, TokenExpiry = expiry });
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
                VerificationToken = Generators.GenerateToken()
            };

            var result = await base.Post(user);

            if (result.IsFailed)
            {
                return Result.Fail<string>(result.Errors);
            }

            return await SendVerificationEmail(user);
        }

        public async Task<Result<string>> Verify(VerificationRequest request)
        {
            User user = await Repository.FindOneAsync(x => x.VerificationToken == request.VerificationToken);

            if (user is null)
            {
                return Result.Fail<string>(BaseErrors.ObjectNotFound<User>());
            }

            if (user.VerifiedAt is not null)
            {
                return Result.Fail<string>(UserErrors.UserVerified());
            }

            user.VerifiedAt = DateTime.Now;
            user.VerificationToken = null;
            
            var result = await base.Update(user);
            if (result.IsFailed)
            {
                return Result.Fail<string>(result.Errors);
            }

            return Result.Ok("Thank you for verifying. Please log in with your email and password");
        }

        public async Task ForgotPassword(EmailAddressRequest request)
        {
            var user = await Repository.FindOneAsync(x => x.EmailAddress == request.EmailAddress);
            if (user is not null)
            {
                user.ResetToken = Generators.GenerateToken();
                user.ResetExpiry = DateTime.Now.AddHours(1);

                await base.Update(user);
                await SendResetEmail(user);
            }
        }

        public async Task <Result<string>> ResetPassword(ResetPasswordRequest request)
        { 
            var user = await Repository.FindOneAsync(x => x.VerificationToken == request.ResetToken && x.ResetExpiry <= DateTime.Now);

            if (user is null)
            {
                return Result.Fail<string>(BaseErrors.ObjectNotFound<User>());
            }

            var info = Generators.GeneratePasswordHash(request.Password);
            user.PasswordSalt = info.salt;
            user.PasswordHash = info.hash;
            user.ResetExpiry = null;
            user.ResetToken = null;

            var result = await base.Update(user);
            if (result.IsFailed)
            {
                return Result.Fail<string>(result.Errors);
            }

            return Result.Ok("Your email has been successfully reset, you may now log in.");
        }

        private async Task<Result<string>> SendVerificationEmail(User user)
        {
            await Task.Delay(1);
            return Result.Ok("Thank you, pLease follow the link in the email to verify your registration");
        }

        private async Task SendResetEmail(User user)
        {
            await Task.Delay(1);
        }
    }
}
