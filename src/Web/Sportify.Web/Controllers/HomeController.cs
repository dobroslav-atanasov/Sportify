namespace Sportify.Web.Controllers
{
    using Constants;
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
            if (this.User.IsInRole(Role.Administrator))
            {
                return this.RedirectToAction("Index", "Home", new {area = AreaConstants.Administrator});
            }

            this.ViewData[GlobalConstants.Countries] = this.countriesService.GetAllCountryNames();
            return this.View();
        }

        public IActionResult AboutUs()
        {
            return this.View();
        }

        public IActionResult Access()
        {
            return this.View();
        }

        public IActionResult Invalid()
        {
            this.ViewData[GlobalConstants.Message] = this.TempData[GlobalConstants.Message].ToString();
            return this.View();
        }
    }
}