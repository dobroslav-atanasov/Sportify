namespace Sportify.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models.Users;

    public class UsersController : Controller
    {
        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            return this.RedirectToAction("Index", "Home");
        }
    }
}