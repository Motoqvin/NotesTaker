using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

public class EmailSender
{
    private readonly IConfiguration configuration;

    public EmailSender(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var emailConfig = configuration.GetSection("EmailSettings");
        var from = emailConfig["From"];
        var smtp = emailConfig["SmtpServer"];
        var port = int.Parse(emailConfig["Port"]);
        var username = emailConfig["Username"];
        var password = emailConfig["Password"];

        using var client = new SmtpClient(smtp, port)
        {
            Credentials = new NetworkCredential(username, password),
            EnableSsl = true
        };

        var message = new MailMessage(from, toEmail, subject, body);
        await client.SendMailAsync(message);
    }
}
