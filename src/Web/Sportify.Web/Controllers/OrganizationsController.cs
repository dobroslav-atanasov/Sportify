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

        public OrganizationsController(IOrganizationsService organizationsService)
        {
            this.organizationsService = organizationsService;
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

            this.organizationsService.Create(model, this.User.Identity.Name);
            return this.RedirectToAction("Index", "Home");
        }
    }
}