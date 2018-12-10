namespace Sportify.Web.Controllers
{
    using Constants;
    using Data.ViewModels.Messages;
    using Microsoft.AspNetCore.Mvc;
    using Services.Interfaces;

    public class HomeController : Controller
    {
        private readonly ICountriesService countriesService;
        private readonly IMessagesService messagesService;

        public HomeController(ICountriesService countriesService, IMessagesService messagesService)
        {
            this.countriesService = countriesService;
            this.messagesService = messagesService;
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
            var isSendMessage = this.messagesService.IsSendMessage(model);
            if (!isSendMessage)
            {
                this.ViewData["Error"] = Constants.UserDoesNotExist;
                return this.View(model);
            }
            else
            {
                this.ViewData["Message"] = Constants.MessageIsSentSuccessfully;
                return this.View(model);
            }
        }
    }
}