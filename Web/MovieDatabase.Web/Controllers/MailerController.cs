namespace MovieDatabase.Web.Controllers
{
    using System;

    using Microsoft.AspNetCore.Mvc;
    using MovieDatabase.Common;
    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Data;

    public class MailerController : BaseController
    {
        private readonly IMailersService mailersService;

        public MailerController(IMailersService mailersService)
        {
            this.mailersService = mailersService;
        }

        [IgnoreAntiforgeryToken]
        [HttpPost]
        public IActionResult SendMessage([FromBody] ContactForm form)
        {
            if (form == null)
            {
                return this.BadRequest();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    if (!this.mailersService.Validate(form.ReCaptcha))
                    {
                        throw new Exception(GlobalConstants.ReCaptchaExc);
                    }
                    else
                    {
                        this.mailersService.SendMessage(form);
                    }

                    return this.Json(new { success = true, message = GlobalConstants.EmailSuccessMessage });
                }
                catch (Exception ex)
                {
                    return this.Json(new { success = false, message = ex.Message });
                }
            }

            return this.BadRequest();
        }
    }
}
