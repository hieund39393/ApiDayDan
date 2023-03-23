using EVN.Core.Interfaces.Email;
using EVN.Core.Interfaces.Logging;
using EVN.Core.Models.Emails;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using static EVN.Core.Common.Constants;

namespace EVN.Core.Implements.Email
{
    public class EmailService : IEmailService
    {
        private readonly IAppLogger _appLogger;
        private readonly IConfiguration _configuration;
        public delegate void SendMailHandler(EmailModel email);
        private readonly SmtpClient _smtpClient;
        private readonly SmtpSetting _smtpSetting;

        public EmailService(IAppLogger appLogger, IConfiguration configuration)
        {
            _appLogger = appLogger;
            _configuration = configuration;
            _smtpSetting = _configuration.GetSection(CONFIG_KEYS.SMTP_SETTING).Get<SmtpSetting>();
            _smtpClient = new SmtpClient
            {
                Host = _smtpSetting.Host,
                EnableSsl = _smtpSetting.EnableSsl,
                Port = Convert.ToInt32(_smtpSetting.Port),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = _smtpSetting.DefaultCredentials,
                Credentials = new BasicNetworkCredential(_smtpSetting.Account, _smtpSetting.Password),
                //Timeout = 60000
            };
        }
        public void Send(EmailModel email)
        {
            var handler = new SendMailHandler(SendMailBackground);
            Task.Run(() =>
            {
                handler(email);
            });
        }
        private void SendMailBackground(EmailModel email)
        {
            try
            {
                //email.Body = await _mailBodyGenerator.GenerateBodyAsync(email.Body, mailComplie);

                var fromAddress = new MailAddress(_smtpSetting.Account, _smtpSetting.SenderName);
                var mailMessage = new MailMessage()
                {
                    From = fromAddress,
                    Subject = email.Subject,
                    IsBodyHtml = true,
                    Body = email.Body,
                    BodyEncoding = Encoding.UTF8,
                    SubjectEncoding = Encoding.UTF8
                };

                // Create the HTML view
                var htmlView = AlternateView.CreateAlternateViewFromString(email.Body, Encoding.UTF8, MediaTypeNames.Text.Html);

                //var imageLinked = EmailLogoLinkedResource();
                //htmlView.LinkedResources.Add(imageLinked);

                mailMessage.AlternateViews.Add(htmlView);

                foreach (var toEmail in email.ToAddresses)
                {
                    mailMessage.To.Add(toEmail);
                }

                if (email.Attachment != null)
                {
                    Stream stream = new MemoryStream(email.Attachment.File);
                    var attachment = new Attachment(stream, email.Attachment.FileName, System.Net.Mime.MediaTypeNames.Application.Octet);

                    if (!string.IsNullOrEmpty(email.Attachment.FileType))
                        attachment.ContentType = new System.Net.Mime.ContentType(email.Attachment.FileType);

                    mailMessage.Attachments.Add(attachment);
                }

                _smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                _appLogger.Error(ex.Message);
            }
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
