namespace Sportify.Web.Areas.Administrator.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Area("Administrator")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}