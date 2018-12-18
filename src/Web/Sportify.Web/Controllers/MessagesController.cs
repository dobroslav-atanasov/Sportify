namespace Sportify.Web.Controllers
{
    using Data.Models;
    using Data.ViewModels.Messages;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services.Interfaces;

    public class MessagesController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IMessagesService messagesService;

        public MessagesController(UserManager<User> userManager, SignInManager<User> signInManager, IMessagesService messagesService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.messagesService = messagesService;
        }

        public IActionResult Send()
        {
            if (this.signInManager.IsSignedIn(this.User))
            {
                var user = this.userManager.FindByNameAsync(this.User.Identity.Name).GetAwaiter().GetResult();
                this.ViewData["Username"] = user.UserName;
                this.ViewData["Email"] = user.Email;
            }
            return this.View();
        }

        [HttpPost]
        public IActionResult Send(SendMessageViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            User user = null;
            if (this.signInManager.IsSignedIn(this.User))
            {
                user = this.userManager.FindByNameAsync(this.User.Identity.Name).GetAwaiter().GetResult();
            }

            this.messagesService.Send(model, user);

            return this.View("Thankyou", "Messages");
        }
    }
}