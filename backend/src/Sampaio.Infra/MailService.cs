using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using Sampaio.Shared.Mail;

namespace Sampaio.Infra
{
    public class MailService : IMailService
    {
        private readonly EmailConfig _config;

        public MailService(EmailConfig config)
        {
            _config = config;
        }

        public void Send(
            string emailTo,
            string subject,
            string bodyMessage,
            bool isBodyHtml = true,
            IEnumerable<string> cc = null,
            IEnumerable<string> bcc = null,
            IEnumerable<string> atch = null,
            IEnumerable<string> replyTo = null,
            IDictionary<string, byte[]> atchBin = null)
        {
            Send(_config.From, _config.FromName, emailTo, subject, bodyMessage, isBodyHtml, cc, bcc, atch, replyTo,
                atchBin);
        }

        public void Send(
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
            IDictionary<string, byte[]> atchBin = null)
        {
            var smtp = GetSmtpClient();
            var credential = (NetworkCredential) smtp.Credentials;

            var mailMessage = PrepareMailMessage(
                from,
                fromName,
                emailTo,
                subject,
                bodyMessage,
                isBodyHtml,
                replyTo: replyTo,
                cc: cc,
                bcc: bcc,
                atch: atch,
                atchBin: atchBin);
            try
            {
                smtp.Send(mailMessage);
            }
            catch (System.Exception e)
            {
                throw new System.Exception(e.InnerException?.Message ?? e.Message);
            }
        }
        
        


        private MailMessage PrepareMailMessage(
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
            IDictionary<string, byte[]> atchBin = null)
        {
            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(from, fromName);
            mailMessage.Body = bodyMessage;
            mailMessage.Subject = subject;
            mailMessage.To.Add(new MailAddress(emailTo));
            mailMessage.IsBodyHtml = isBodyHtml;
            mailMessage.Priority = MailPriority.High;
            mailMessage.Subject = subject;

            if (replyTo != null)
                foreach (var item in replyTo)
                    mailMessage.ReplyToList.Add(new MailAddress(item));

            if (cc != null)
                foreach (var item in cc)
                    mailMessage.CC.Add(new MailAddress(item));

            if (bcc != null)
                foreach (var item in bcc)
                    mailMessage.Bcc.Add(new MailAddress(item));

            if (atch != null)
                foreach (var item in atch)
                    mailMessage.Attachments.Add(new Attachment(item));

            if (atchBin != null)
                foreach (var item in atchBin)
                    mailMessage.Attachments.Add(new Attachment(new MemoryStream(item.Value), item.Key));

            return mailMessage;
        }

        private SmtpClient GetSmtpClient()
        {
            var settings = _config;
            var smtpClient = new SmtpClient();
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(settings.UserName, settings.Password);
            smtpClient.Host = settings.Host;
            smtpClient.Port = settings.Port;
            smtpClient.EnableSsl = settings.EnableSsl;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            return smtpClient;
        }
    }
}