namespace Authentication.Application.Model.Mail
{
    public class MailSettings
    {
        public string From { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public bool EnableSsl { get; set; }
        public bool DefaultCredentials { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
