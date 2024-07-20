using RingoMedia.BLL.ViewModels.EmailVM;

namespace RingoMedia.BLL.Managers.Mails
{
    public interface IMailService
    {
        Task SendEmailAsync(SendEmail Email, DateTime dateTime);
    }
}
