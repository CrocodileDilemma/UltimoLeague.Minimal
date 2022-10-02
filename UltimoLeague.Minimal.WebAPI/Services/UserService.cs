using FastEndpoints.Security;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading;
using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.DAL.Interfaces;
using UltimoLeague.Minimal.WebAPI.Errors;
using UltimoLeague.Minimal.WebAPI.Messages;
using UltimoLeague.Minimal.WebAPI.Models;
using UltimoLeague.Minimal.WebAPI.Services.Interfaces;
using UltimoLeague.Minimal.WebAPI.Utilities;

namespace UltimoLeague.Minimal.WebAPI.Services
{
    public class UserService : BaseService<User>
    {
        private readonly IEmailService _emailService;
        public UserService(IMongoRepository<User> repository, IEmailService emailService) : base(repository) 
        {
            _emailService = emailService;
        }

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

        public async Task<Result<MessageDto>> Register(SessionRequest request, CancellationToken cancellationToken)
        {
            User user = await Repository.FindOneAsync(x => x.EmailAddress == request.EmailAddress);
            
            if (user is not null)
            {
                return Result.Fail<MessageDto>(BaseErrors.ObjectExists<User>());
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
                return Result.Fail<MessageDto>(result.Errors);
            }

            await _emailService.SendVerificationEmail(user, cancellationToken);
            return Result.Ok(UserMessages.Registration);
        }

        public async Task<Result<MessageDto>> Verify(VerificationRequest request)
        {
            User user = await Repository.FindOneAsync(x => x.VerificationToken == request.VerificationToken);

            if (user is null)
            {
                return Result.Fail<MessageDto>(BaseErrors.ObjectNotFound<User>());
            }

            if (user.VerifiedAt is not null)
            {
                return Result.Fail<MessageDto>(UserErrors.UserVerified());
            }

            user.VerifiedAt = DateTime.Now;
            user.VerificationToken = null;
            
            var result = await base.Update(user);
            if (result.IsFailed)
            {
                return Result.Fail<MessageDto>(result.Errors);
            }

            return Result.Ok(UserMessages.Verification);
        }

        public async Task ForgotPassword(string emailAddress, CancellationToken cancellationToken)
        {
            var user = await Repository.FindOneAsync(x => x.EmailAddress == emailAddress);
            if (user is not null)
            {
                user.ResetToken = Generators.GenerateToken();
                user.ResetExpiry = DateTime.Now.AddHours(1);

                await base.Update(user);
                await _emailService.SendResetEmail(user, cancellationToken);
            }
        }

        public async Task <Result<MessageDto>> ResetPassword(ResetPasswordRequest request)
        { 
            var user = await Repository.FindOneAsync(x => x.ResetToken == request.ResetToken && x.ResetExpiry >= DateTime.Now);

            if (user is null)
            {
                return Result.Fail<MessageDto>(BaseErrors.ObjectNotFound<User>());
            }

            var info = Generators.GeneratePasswordHash(request.Password);
            user.PasswordSalt = info.salt;
            user.PasswordHash = info.hash;
            user.ResetExpiry = null;
            user.ResetToken = null;

            var result = await base.Update(user);
            if (result.IsFailed)
            {
                return Result.Fail<MessageDto>(result.Errors);
            }

            return Result.Ok(UserMessages.ResetPassword);
        }

        internal void GenerateAdminUser()
        {
            var user = Repository.FindOne(x => x.AdminUser);
            if (user is null)
            {
                var info = Generators.GeneratePasswordHash("Admin123");

                user = new User
                {
                    EmailAddress = _emailService.GetEmailAddress(),
                    PasswordHash = info.hash,
                    PasswordSalt = info.salt,
                    AdminUser = true,
                    VerifiedAt = DateTime.Now
                };

                base.Post(user);
            }
        }
    }
}
