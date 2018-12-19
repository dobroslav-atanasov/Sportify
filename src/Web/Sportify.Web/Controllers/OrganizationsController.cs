namespace Sportify.Web.Controllers
{
    using Data.ViewModels.Organizations;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.Interfaces;
    using Constants;
    using X.PagedList;
    using Sportify.Web.Constants;

    public class OrganizationsController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IOrganizationsService organizationsService;

        public OrganizationsController(IOrganizationsService organizationsService)
        {
            this.organizationsService = organizationsService;
        }

        [Authorize(Roles = Role.Editor)]
        public IActionResult Create()
        {
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
    }
}