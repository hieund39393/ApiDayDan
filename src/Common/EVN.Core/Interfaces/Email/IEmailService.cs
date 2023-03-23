using EVN.Core.Models.Emails;

namespace EVN.Core.Interfaces.Email
{
    public interface IEmailService
    {
        /// <summary>
        /// Send email
        /// </summary>
        /// <param name="emailModel"></param>
        void Send(EmailModel email);
    }
}
