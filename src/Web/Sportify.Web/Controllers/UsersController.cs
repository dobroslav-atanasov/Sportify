namespace Sportify.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models.Users;
    using Services.Interfaces;

    public class UsersController : Controller
    {
        private readonly ICountriesService countriesService;

        public UsersController(ICountriesService countriesService)
        {
            this.countriesService = countriesService;
        }

        public IActionResult Register()
        {
            this.ViewData["Countries"] = this.countriesService.GetAllCountryNames();
            return this.View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.ViewData["Countries"] = this.countriesService.GetAllCountryNames();
                return this.View(model);
            }

            return this.RedirectToAction("Index", "Home");
        }
    }
}