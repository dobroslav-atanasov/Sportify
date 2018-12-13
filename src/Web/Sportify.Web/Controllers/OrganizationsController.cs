namespace Sportify.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Sportify.Data.Models;
    using Sportify.Data.ViewModels.Organizations;
    using Sportify.Services.Interfaces;

    public class OrganizationsController : Controller
    {
        private readonly IOrganizationsService organizationsService;
        private readonly UserManager<User> userManager;

        public OrganizationsController(IOrganizationsService organizationsService, UserManager<User> userManager)
        {
            this.organizationsService = organizationsService;
            this.userManager = userManager;
        }

        [Authorize(Roles = Constants.Constants.EditorRole)]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = Constants.Constants.EditorRole)]
        public IActionResult Create(CreateOrganizationViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = this.userManager.FindByNameAsync(this.User.Identity.Name).GetAwaiter().GetResult();
            this.organizationsService.Create(model, user);
            return this.RedirectToAction("Index", "Home");
        }
    }
}