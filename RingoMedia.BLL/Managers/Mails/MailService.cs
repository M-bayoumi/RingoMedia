using Hangfire;
using Microsoft.Extensions.Options;
using RingoMedia.BLL.ViewModels.EmailVM;
using RingoMedia.DAL.Common.Settings;
using System.Net;
using System.Net.Mail;

namespace RingoMedia.BLL.Managers.Mails;

public class MailService : IMailService
{
    private readonly MailSettings _mailSettings;

    public MailService(IOptions<MailSettings> mailSettings)
    {
        _mailSettings = mailSettings.Value;
    }

    public async Task SendEmailAsync(SendEmail email, DateTime dateTime)
    {
        BackgroundJob.Schedule(() => SendEmail(email), dateTime - DateTime.Now);
    }

    public async Task SendEmail(SendEmail email)
    {
        var mailMessage = new MailMessage
        {
            From = new MailAddress(_mailSettings.Email, _mailSettings.DisplayName),
            Subject = email.Subject,
            Body = email.Body,
            IsBodyHtml = true
        };

        mailMessage.To.Add(new MailAddress(email.MailTo));

        using var smtpClient = new SmtpClient(_mailSettings.Host, _mailSettings.Port)
        {
            Credentials = new NetworkCredential(_mailSettings.Username, _mailSettings.Password),
            EnableSsl = true
        };

        await smtpClient.SendMailAsync(mailMessage);
    }
}
