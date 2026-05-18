using MailKit.Net.Smtp;
using MimeKit;

namespace StudentAPI.Services.Email
{
    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse("studentprojectdemo33@gmail.com"));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;

            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = body
            };

            using var smtp = new SmtpClient();

            await smtp.ConnectAsync(
                "smtp.gmail.com",
                587,
                MailKit.Security.SecureSocketOptions.StartTls
            );

            await smtp.AuthenticateAsync(
                "studentprojectdemo33@gmail.com",
                "qesi pksm ohdj ptoe"
            );

            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}