using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationFormSyncer.Services
{
    public class EmailService
    {
        //
        public void SendWelcomeMessage(string to, string Subject, string body)
        {
            MailMessage message = new MailMessage();
            //message.From = new MailAddress("enockbwana@gmail.com");
            message.From = new MailAddress("booking@checkupsmed.com");
            message.To.Add(new MailAddress(to));
            message.Bcc.Add(new MailAddress("pcrtest@checkupsmed.com"));
            message.Subject = Subject;
            message.Body = body;
            message.IsBodyHtml = true;
            // message.Attachments.Add(new Attachment(pdfStream, "Results.pdf"));

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("booking@checkupsmed.com", "checkups@123"),
                EnableSsl = true,
            };
            // send email
            smtpClient.Send(message);

        }
        public void SendSampleCollectionEmail(string to, string Subject, string body)
        {
            MailMessage message = new MailMessage();
            //message.From = new MailAddress("enockbwana@gmail.com");
            message.From = new MailAddress("booking@checkupsmed.com");
            message.To.Add(new MailAddress(to));
            message.Subject = Subject;
            message.Body = body;
            message.IsBodyHtml = true;
            // message.Attachments.Add(new Attachment(pdfStream, "Results.pdf"));

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("booking@checkupsmed.com", "checkups@123"),
                EnableSsl = true,
            };
            // send email
            smtpClient.Send(message);

        }

    }
}
