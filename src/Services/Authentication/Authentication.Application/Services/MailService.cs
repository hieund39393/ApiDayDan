using Authentication.Application.Model.Mail;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;
using static EVN.Core.Common.AppConstants;

namespace Authentication.Application.Services
{
    public interface IMailService
    {
        bool SendMail(string to, string[] bcc, string subject, string body
            , Stream[] attachments = null, string fileName = null, string fileType = null);
    }

    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// Send email
        /// </summary>
        /// <param name="to"></param>
        /// <param name="bcc"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="attachments"></param>
        /// <param name="fileName"></param>
        /// <param name="fileType"></param>
        /// <returns></returns>
        public bool SendMail(string to, string[] bcc, string subject, string body
            , Stream[] attachments = null, string fileName = null, string fileType = null)
        {
            var mailSettings = _configuration.GetSection(Settings.MailSettings)
                .Get<MailSettings>();

            using (var client = new SmtpClient(mailSettings.Host))
            {
                client.Host = mailSettings.Host;
                if (!string.IsNullOrEmpty(mailSettings.Port))
                {
                    client.Port = int.Parse(mailSettings.Port);
                }
                client.EnableSsl = Convert.ToBoolean(mailSettings.EnableSsl);
                client.UseDefaultCredentials = Convert.ToBoolean(mailSettings.DefaultCredentials);
                client.Credentials = new BasicNetworkCredential(mailSettings.UserName, mailSettings.Password);

                List<Attachment> atts = new List<Attachment>();
                if (attachments != null && attachments.Any())
                {
                    foreach (var att in attachments)
                    {
                        var attach = new Attachment(att, fileName, fileType);
                        atts.Add(attach);
                    }
                }

                using var msg = new MailMessage();
                var view = AlternateView.CreateAlternateViewFromString(body, Encoding.UTF8,
                    "text/html");
                msg.From = new MailAddress(mailSettings.From);
                msg.Sender = new MailAddress(mailSettings.From);
                msg.AlternateViews.Add(view);
                msg.IsBodyHtml = true;
                msg.SubjectEncoding = Encoding.UTF8;
                msg.BodyEncoding = Encoding.UTF8;
                msg.Subject = subject;
                msg.Body = body;
                msg.To.Add(to);
                if (bcc != null && bcc.Any())
                {
                    foreach (var email in bcc)
                    {
                        msg.Bcc.Add(email);
                    }
                }
                client.Send(msg);
            }
            return true;
        }

        private class BasicNetworkCredential : ICredentialsByHost
        {
            private readonly NetworkCredential _wrappedNetworkCredential;
            public BasicNetworkCredential(string username, string password)
            {
                _wrappedNetworkCredential = new NetworkCredential(username, password);
            }

            public NetworkCredential GetCredential(string host, int port, string authenticationType)
            {
                return authenticationType.ToLower() != "login" ? null : _wrappedNetworkCredential.GetCredential(host, port, authenticationType);
            }
        }
    }

    
}
