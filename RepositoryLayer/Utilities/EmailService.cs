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
    public static class EmailService
    { 
        public static string SendRegisterMail(EmailML model)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("anonymous.u2003@gmail.com"));
            email.To.Add(MailboxAddress.Parse(model.Email));
            email.Subject = "Welcome to Our E-Insurance App Service!";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = $@"Hello {model.Name},

                Thank you for registering with us. We're excited to have you on board!

                Here are your account details:
                Email: {model.Email}
                Password: {PasswordHashing.Decrypt(model.Password)}

                If you have any questions or need assistance, feel free to contact our support team.

                Best regards,
                The E-Insurance App Team"
            };
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("anonymous.u2003@gmail.com", "iilw zyld hjuw bktf");
            smtp.Send(email);
            smtp.Disconnect(true);
            return "Mail sent successfully";
        }
    }
}