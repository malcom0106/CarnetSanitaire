using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarnetSanitaire.Web.UI.Services
{
    public class EmailSender : IEmailSender
    {       

        public string SmtpServeur { get; set; }
        public string SmtpPort { get; set; }
        public string SmtpIdentifiant { get; set; }
        public string SmtpMotDePasse { get; set; }

        public EmailSender(string smtpServeur, string smtpPort, string smtpIdentifiant, string smtpMotDePasse)
        {
            SmtpServeur = smtpServeur;
            SmtpPort = smtpPort;
            SmtpIdentifiant = smtpIdentifiant;
            SmtpMotDePasse = smtpMotDePasse;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {

            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("", SmtpIdentifiant));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("html") { Text = htmlMessage };
            try
            {
                var client = new SmtpClient();
                //     {
                // client.Connect("smtp.gmail.com", 587, SecureSocketOptions.Auto);
                // client.Authenticate(_settings.Value.Email, _settings.Value.Password);
                //  client.Send(emailMessage);
                // client.Disconnect(true);
                await client.ConnectAsync(SmtpServeur, Convert.ToInt32(SmtpPort), SecureSocketOptions.Auto);
                await client.AuthenticateAsync(SmtpIdentifiant, SmtpMotDePasse);

                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
                //  }
            }
            catch (Exception ex) //todo add another try to send email
            {
                var e = ex;
                throw;
            }
        }
    }
}
