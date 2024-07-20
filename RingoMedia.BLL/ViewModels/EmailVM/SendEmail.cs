using System.ComponentModel.DataAnnotations;

namespace RingoMedia.BLL.ViewModels.EmailVM;

public class SendEmail
{
    [Required]
    public string MailTo { get; set; } = string.Empty;
    [Required]
    public string Subject { get; set; } = string.Empty;
    [Required]
    public string Body { get; set; } = string.Empty;
    [Required]
    public DateTime DateTime { get; set; } = DateTime.Now;
}

