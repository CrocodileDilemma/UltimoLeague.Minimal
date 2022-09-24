namespace UltimoLeague.Minimal.DAL.Interfaces
{
    public interface IEmailSettings
    {
        string SmtpPass { get; set; }
        int SmtpPort { get; set; }
        string SmtpSender { get; set; }
        string SmtpSenderName { get; set; }
        string SmtpHost { get; set; }
        string SmtpUser { get; set; }
    }
}