namespace Sportify.Web.Areas.Administrator.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.Interfaces;

    [Area("Administrator")]
    public class HomeAdminController : Controller
    {
        private readonly IUsersService usersService;

        public HomeAdminController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Index()
        {
            return this.View();
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult SignOut()
        {
            this.usersService.SignOut();
            return this.RedirectToAction("Index", "Home");
        }
    }
}