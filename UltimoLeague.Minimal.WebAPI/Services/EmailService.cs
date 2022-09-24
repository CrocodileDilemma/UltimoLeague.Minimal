using MailKit.Security;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.DAL.Interfaces;
using UltimoLeague.Minimal.WebAPI.Services.Interfaces;
using UltimoLeague.Minimal.WebAPI.Messages;

namespace UltimoLeague.Minimal.WebAPI.Services
{
    public class EmailService : IEmailService
    {
        private readonly IEmailSettings _settings;
        public EmailService(IEmailSettings settings)
        {
            _settings = settings;
        }

        public async Task SendResetEmail(User user, CancellationToken cancellationToken)
        {
            var email = this.GenerateEmail(user);
            email.Subject = UserMessages.ResetPasswordSubject.Message;
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = string.Format(UserMessages.ResetPasswordBody.Message, user.EmailAddress, user.ResetToken)
            };

            await SendEmail(email, cancellationToken);
        }

        public async Task SendVerificationEmail(User user, CancellationToken cancellationToken)
        {
            var email = this.GenerateEmail(user);
            email.Subject = UserMessages.VerificationSubject.Message;
            email.Body = new TextPart(TextFormat.Html) 
            { 
                Text = string.Format(UserMessages.VerificationBody.Message, user.EmailAddress, user.VerificationToken) 
            };

            await SendEmail(email, cancellationToken);
        }

        private async Task SendEmail(MimeMessage email, CancellationToken cancellationToken)
        {
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_settings.SmtpHost, _settings.SmtpPort, SecureSocketOptions.StartTls, cancellationToken);
            await smtp.AuthenticateAsync(_settings.SmtpUser, _settings.SmtpPass, cancellationToken);
            await smtp.SendAsync(email, cancellationToken);
            await smtp.DisconnectAsync(true, cancellationToken);
        }

        private MimeMessage GenerateEmail(User user)
        {
            var email = new MimeMessage();
            var address = MailboxAddress.Parse(_settings.SmtpSender);
            address.Name = _settings.SmtpSenderName;
            email.From.Add(address);
            email.To.Add(MailboxAddress.Parse(user.EmailAddress));
            return email;
        }
    }
}
