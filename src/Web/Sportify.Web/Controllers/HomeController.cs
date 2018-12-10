namespace Sportify.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services.Interfaces;

    public class HomeController : Controller
    {
        private readonly ICountriesService countriesService;

        public HomeController(ICountriesService countriesService)
        {
            this.countriesService = countriesService;
        }

        public IActionResult Index()
        {
            if (this.User.IsInRole("Administrator"))
            {
                return this.RedirectToAction("Index", "HomeAdmin", new {area = "Administrator"});
            }

            this.ViewData["Countries"] = this.countriesService.GetAllCountryNames();
            return this.View();
        }

        public IActionResult AboutUs()
        {
            return this.View();
        }
    }
}