namespace Sportify.Web.Controllers
{
    using Constants;
    using Data.ViewModels.Messages;
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

        public IActionResult ContactUs()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult ContactUs(AddMessageViewModel model)
        {
            User user = null;
            if (this.signInManager.IsSignedIn(this.User))
            {
                user = this.userManager.FindByNameAsync(this.User.Identity.Name).GetAwaiter().GetResult();
            }

            this.messagesService.SendMessage(model, user);

            this.ViewData["Message"] = Constants.MessageIsSentSuccessfully;
            return this.View(model);
        }

        public IActionResult Access()
        {
            return this.View();
        }
    }
}