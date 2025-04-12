using DesafioBtg.Dominio.Emails.Repositorios.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;

namespace DesafioBtg.Infra.Emails.Repositorios;
public class EmailsRepositorio : IEmailsRepositorio
{
    private readonly IConfiguration configuration;

    public EmailsRepositorio(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public async Task EnviarEmailAsync(string destinatario, string assunto, string mensagem)
    {
        var emailConfig = configuration.GetSection("EmailSettingsYahoo");

        using (var client = new SmtpClient(emailConfig["SmtpServer"], int.Parse(emailConfig["SmtpPort"])))
        {
            client.Credentials = new NetworkCredential(emailConfig["Username"], emailConfig["Password"]);

            client.EnableSsl = bool.Parse(emailConfig["EnableSsl"]);

            client.UseDefaultCredentials = false;

            var mailMessage = new MailMessage
            {
                From = new MailAddress(emailConfig["SenderEmail"], emailConfig["SenderName"]),
                Subject = assunto,
                Body = mensagem,
                IsBodyHtml = true,
                Priority = MailPriority.High
            };

            mailMessage.To.Add(destinatario);

            await client.SendMailAsync(mailMessage);
        }
    }
}
