namespace Sportify.Web.Areas.Administrator.Controllers
{
    using Constants;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Area(AreaConstants.Administrator)]
    public class HomeController : Controller
    {
        [Authorize(Roles = Role.Administrator)]
        public IActionResult Index()
        {
            return this.View();
        }
    }
}