using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using ModelLayer;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Utilities
{
    public class EmailService
    {
        
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }


        public string SendRegisterMail(EmailML model)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("anonymous.u2003@gmail.com"));
            email.To.Add(MailboxAddress.Parse(model.Email));
            email.Subject = "Welcome to Our E-Insurance App Service!";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = $@"Hello {model.Email},

                Thank you for registering with us. We're excited to have you on board!

                Here are your account details:
                Email: {model.Email}
                Password: {model.Password}

                Please log in and change your password immediately for security reasons.

                If you have any questions or need assistance, feel free to contact our support team.

                Best regards,
                The E-Insurance App Team"
            };
            using var smtp = new SmtpClient();
            smtp.Connect(_config["Email:Host"], 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_config["Email:UserName"], _config["Email:Password"]);
            smtp.Send(email);
            smtp.Disconnect(true);
            return "Mail sent successfully";
        }

    }
}