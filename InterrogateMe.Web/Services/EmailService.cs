using InterrogateMe.Core.Data;
using InterrogateMe.Core.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace InterrogateMe.Web.Services
{
    public class EmailService : IEmailService
    {
        #region Private Variables

        private readonly string _emailAddress = Environment.GetEnvironmentVariable("PCIE", EnvironmentVariableTarget.User);
        private readonly string _password = Environment.GetEnvironmentVariable("PCIP", EnvironmentVariableTarget.User);
        private const string _smtpServer = "smtp.gmail.com";
        private const int _portNumber = 587;

        #endregion

        #region Public Methods

        public async Task SendEmailAsync(Sender sender)
        {

            try
            {
                using (var mail = new MailMessage(from: new MailAddress(sender.Email, sender.Name), to: new MailAddress(_emailAddress))
                {
                    Sender = new MailAddress(sender.Email, sender.Name),
                    Body = $"Email Address : {sender.Email}{Environment.NewLine}{sender.Message}",
                    IsBodyHtml = false
                })
                {
                    mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                    using (var smtpClient = new SmtpClient(_smtpServer, _portNumber))
                    {
                        smtpClient.EnableSsl = true;
                        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = new NetworkCredential(_emailAddress, _password);
                        await smtpClient.SendMailAsync(mail);
                    }
                }
            }
            catch (SmtpException exception)
            {
                throw new SmtpException("There was an error", exception);
            }
        }
        #endregion
    }
}
