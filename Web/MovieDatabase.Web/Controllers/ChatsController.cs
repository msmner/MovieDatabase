namespace MovieDatabase.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ChatsController : BaseController
    {
        [Authorize]
        public IActionResult Chat()
        {
            return this.View();
        }
    }
}
