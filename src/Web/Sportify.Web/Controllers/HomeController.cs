namespace Sportify.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (this.User.IsInRole("Administrator"))
            {
                return this.RedirectToAction("Index", "HomeAdmin", new {area = "Administrator"});
            }

            return this.View();
        }
    }
}