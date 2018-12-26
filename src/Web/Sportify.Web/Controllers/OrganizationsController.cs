namespace Sportify.Web.Controllers
{
    using Constants;
    using Data.Models;
    using Data.ViewModels.Organizations;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services.Interfaces;
    using X.PagedList;

    public class OrganizationsController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IOrganizationsService organizationsService;

        public OrganizationsController(UserManager<User> userManager, IOrganizationsService organizationsService)
        {
            this.userManager = userManager;
            this.organizationsService = organizationsService;
        }

        [Authorize(Roles = Role.Editor)]
        public IActionResult Create()
        {
            var user = this.userManager.GetUserAsync(this.User).GetAwaiter().GetResult();
            var isOrganization = this.organizationsService.CheckUserHasOrganization(user.UserName);

            if (isOrganization)
            {
                return this.View("Access", "Organizations");
            }

            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = Role.Editor)]
        public IActionResult Create(CreateOrganizationViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            this.organizationsService.Create(model, this.User.Identity.Name);
            return this.RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = Role.Administrator)]
        public IActionResult All(int? page)
        {
            var organizations = this.organizationsService.GetAllOrganizations();

            var pageNumber = page ?? 1;
            var organizationsOnPage = organizations.ToPagedList(pageNumber, 10);

            return this.View(organizationsOnPage);
        }

        [Authorize(Roles = Role.Editor)]
        public IActionResult Edit()
        {
            var user = this.userManager.GetUserAsync(this.User).GetAwaiter().GetResult();
            var isOrganization = this.organizationsService.CheckUserHasOrganization(user.UserName);

            if (!isOrganization)
            {
                return this.RedirectToAction("Create", "Organizations", new { area = AreaConstants.Base });
            }

            var organization = this.organizationsService.GetOrganizationByUser(user.UserName);
            return this.View(organization);
        }

        [HttpPost]
        [Authorize(Roles = Role.Editor)]
        public IActionResult Edit(OrganizationViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var organization = this.organizationsService.UpdateOrganization(model);
            if (organization == null)
            {
                this.ViewData[GlobalConstants.Error] = GlobalConstants.OrganizationWasNotUpdated;
                return this.View(model);
            }

            this.ViewData[GlobalConstants.Message] = GlobalConstants.OrganizationWasUpdated;
            return this.View();
        }
    }
}