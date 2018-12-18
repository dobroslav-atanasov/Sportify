namespace Sportify.Web.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services.Interfaces;
    using Sportify.Data.Models;

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

        public IActionResult Access()
        {
            return this.View();
        }
    }
}