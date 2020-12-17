namespace MovieDatabase.Web.Controllers
{
    using System;
    using System.Net;
    using System.Net.Mail;

    using Microsoft.AspNetCore.Mvc;
    using MovieDatabase.Data.Models;
    using Newtonsoft.Json;

    public class MailerController : BaseController
    {
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
                    // Enter your email address and password
                    var credentials = new NetworkCredential("ivaylo.nikolov88@gmail.com", "Manotan88");

                    var mail = new MailMessage()
                    {
                        From = new MailAddress("ivaylo.nikolov88@gmail.com"), // Enter your email address
                        Subject = "Website Inquiry",
                        Body = this.FormattedBody(form.Name, form.Email, form.Phone, form.Message),
                    };

                    mail.IsBodyHtml = true;
                    mail.To.Add(new MailAddress("ivaylo.nikolov88@gmail.com")); // Enter your email address

                    // You may have to tweak these settings depending on your mail server's requirements
                    var client = new SmtpClient()
                    {
                        UseDefaultCredentials = false,
                        Host = "smtp.gmail.com", // Enter your mail server host
                        Credentials = credentials,
                        Port = 587,
                        EnableSsl = true,
                    };

                    if (!this.Validate(form.ReCaptcha))
                    {
                        throw new Exception("The submission failed the spam bot verification. If you have " +
                            "JavaScript disabled in your browser, please enable it and try again.");
                    }
                    else
                    {
                        client.Send(mail);
                    }

                    return this.Json(new { success = true, message = "Your message was successfully sent." });
                }
                catch (Exception ex)
                {
                    return this.Json(new { success = false, message = ex.Message });
                }
            }

            return this.BadRequest();
        }

        private string FormattedBody(string name, string email, string phone, string message)
        {
            var senderInfo = string.Format(
                "<b>From</b>: {0}<br/><b>Email</b>: {1}<br/><b>Phone</b>: {2}<br/><br/>",
                name,
                email,
                phone);
            return senderInfo + message;
        }

        private bool Validate(string response)
        {
            using (var client = new System.Net.WebClient())
            {
                try
                {
                    string secretKey = "6LeZLeMZAAAAABZsAaxVqvK8FrRJp8MF2Gkm_rNC";
                    var reply = client.DownloadString(string.Format(
                            "https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}",
                            secretKey,
                            response));

                    var jsonReturned = JsonConvert.DeserializeObject<ReCaptcha>(reply);
                    return jsonReturned.Success.ToLower() == "true";
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
