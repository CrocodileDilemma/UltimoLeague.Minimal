namespace UltimoLeague.Minimal.WebAPI.Messages
{
    public static class UserMessages
    {
        public static MessageDto Verification = new MessageDto
        {
            Message = "Thank you for verifying your account. You may now log in with your email and password."
        };

        public static MessageDto ResetPassword = new MessageDto
        {
            Message = "Your password has been successfully reset, you may now log in."
        };

        public static MessageDto Registration = new MessageDto
        {
            Message = "Thank you for registering, please follow the link in the email to verify your registration."
        };

        public static MessageDto ForgotPassword = new MessageDto
        {
            Message = "An email containing a reset link has been sent."
        };

        public static MessageDto ResetPasswordSubject = new MessageDto
        {
            Message = "Ultimo League Password Reset"
        };

        public static MessageDto ResetPasswordBody = new MessageDto
        {
            Message = @"<b>Dear {0}</b></br>
                <p>Please follow the link below to reset your password:</p></br>
                <b>{1}</br>"
        };

        public static MessageDto VerificationSubject = new MessageDto
        {
            Message = "Ultimo League Account Verification"
        };

        public static MessageDto VerificationBody = new MessageDto
        {
            Message = @"<b>Dear {0}</b></br>
                <p>Thank you for Registering with Ultimo League. 
                Please follow the link below to verify your registration:</p></br>
                <b>{1}</br>"
        };
    }
}
