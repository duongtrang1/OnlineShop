using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;

namespace Common
{
    public class MailHelper
    {
        public void SendMail(string subject, string content,string body)
        {
            var fromEmailAddress = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();
            var fromEmailDisplayName = ConfigurationManager.AppSettings["FromEmailDisplayName"].ToString();
            var fromEmailPassword = ConfigurationManager.AppSettings["FromEmailPassword"].ToString();
            var toEmailAddress = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();
            var smtpHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
            var smtpPort = ConfigurationManager.AppSettings["SMTPPort"].ToString();

            bool enabledSsl = bool.Parse(ConfigurationManager.AppSettings["EnabledSSL"].ToString());

             // body = content;
             MailMessage message = new MailMessage(new MailAddress(fromEmailAddress, fromEmailDisplayName), new MailAddress(toEmailAddress))
            {
                Subject = subject,
                IsBodyHtml = true,
                Body = body
            };

            var client = new SmtpClient
            {
                Credentials = new NetworkCredential(fromEmailAddress, fromEmailPassword),
                Host = smtpHost,
                EnableSsl = enabledSsl,
                Port = !string.IsNullOrEmpty(smtpPort) ? Convert.ToInt32(smtpPort) : 0
            };
            client.Send(message);
        }
    }
}
