namespace Sportify.Web.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services.Interfaces;
    using Sportify.Data.Models;
    using Constants;

    public class HomeController : Controller
    {
        private readonly ICountriesService countriesService;
        private readonly IMessagesService messagesService;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public HomeController(ICountriesService countriesService, IMessagesService messagesService, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.countriesService = countriesService;
            this.messagesService = messagesService;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Index()
        {
            if (this.User.IsInRole(Role.Administrator))
            {
                return this.RedirectToAction("Index", "HomeAdmin", new {area = AreaConstants.Administrator});
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
    }
}