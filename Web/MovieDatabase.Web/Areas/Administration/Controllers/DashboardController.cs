using Microsoft.AspNetCore.Mvc;

namespace MovieDatabase.Web.Areas.Administration.Controllers
{
    public class DashboardController : AdministrationController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
