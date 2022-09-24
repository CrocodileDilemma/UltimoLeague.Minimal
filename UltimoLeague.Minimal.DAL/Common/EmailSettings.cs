using UltimoLeague.Minimal.DAL.Interfaces;

namespace UltimoLeague.Minimal.DAL.Common
{
    public class EmailSettings : IEmailSettings
    {
        public string SmtpPass { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpSender { get; set; }
        public string SmtpSenderName { get; set; }
        public string SmtpHost { get; set; }
        public string SmtpUser { get; set; }
    }
}
