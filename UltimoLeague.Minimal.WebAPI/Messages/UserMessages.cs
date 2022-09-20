namespace UltimoLeague.Minimal.WebAPI.Messages
{
    public static class UserMessages
    {
        public static MessageDto Verification = new MessageDto
        {
            Message = "Thank you for verifying. Please log in with your email and password."
        };

        public static MessageDto ResetPassword = new MessageDto
        {
            Message = "Your email has been successfully reset, you may now log in."
        };

        public static MessageDto VerificationEmail = new MessageDto
        {
            Message = "Thank you, pLease follow the link in the email to verify your registration."
        };

        public static MessageDto ForgotPassword = new MessageDto
        {
            Message = "An email containing a reset link has been sent."
        };
    }
}
