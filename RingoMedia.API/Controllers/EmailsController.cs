using Microsoft.AspNetCore.Mvc;
using RingoMedia.BLL.Managers.Mails;
using RingoMedia.BLL.ViewModels.EmailVM;

namespace RingoMedia.API.Controllers
{
    public class EmailsController : Controller
    {
        IMailService _mailService;

        public EmailsController(IMailService mailService)
        {
            _mailService = mailService;
        }

        [HttpGet]
        public async Task<IActionResult> Send()
        {
            return View("Send");
        }


        [HttpPost]
        public async Task<IActionResult> Send(SendEmail email)
        {
            if (ModelState.IsValid)
            {
                await _mailService.SendEmailAsync(email, email.DateTime);
                return RedirectToAction("Send");
            }
            return View("Send", email);
        }
    }
}
