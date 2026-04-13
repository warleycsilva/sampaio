using System.Collections.Generic;

namespace Sampaio.Shared.Mail
{
    public interface IMailService
    {
        void Send(
            string emailTo,
            string subject,
            string bodyMessage,
            bool isBodyHtml = false,
            IEnumerable<string> cc = null,
            IEnumerable<string> bcc = null,
            IEnumerable<string> atch = null,
            IEnumerable<string> replyTo = null,
            IDictionary<string, byte[]> atchBin = null);

        void Send(
            string from,
            string fromName,
            string emailTo,
            string subject,
            string bodyMessage,
            bool isBodyHtml = true,
            IEnumerable<string> cc = null,
            IEnumerable<string> bcc = null,
            IEnumerable<string> atch = null,
            IEnumerable<string> replyTo = null,
            IDictionary<string, byte[]> atchBin = null);
    }
}
