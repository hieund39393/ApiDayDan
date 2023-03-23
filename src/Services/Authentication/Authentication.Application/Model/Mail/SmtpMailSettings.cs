namespace Authentication.Application.Model.Mail
{
    public class SmtpMailSettings
    {
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public int SmtpTimeOut { get; set; }
        public string SmtpNoReply { get; set; }
        public string SmtpNoReplyPassword { get; set; }
        public string DefaultEmail { get; set; }
        public string AlliasCompanyName { get; set; }
        public bool EnableSsl { get; set; }
    }
}
