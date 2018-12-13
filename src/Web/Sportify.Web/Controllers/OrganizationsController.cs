namespace Sportify.Web.Controllers
{
    using Data.ViewModels.Organizations;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.Interfaces;
    using Constants;
    using X.PagedList;

    public class OrganizationsController : Controller
    {
        private readonly IOrganizationsService organizationsService;

        public OrganizationsController(IOrganizationsService organizationsService)
        {
            this.organizationsService = organizationsService;
        }

        [Authorize(Roles = Constants.EditorRole)]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = Constants.EditorRole)]
        public IActionResult Create(CreateOrganizationViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            this.organizationsService.Create(model, this.User.Identity.Name);
            return this.RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = Constants.AdministratorRole)]
        public IActionResult All(int? page)
        {
            var organizations = this.organizationsService.GetAllOrganizations();

            var pageNumber = page ?? 1;
            var organizationsOnPage = organizations.ToPagedList(pageNumber, 10);

            return this.View(organizationsOnPage);
        }
    }
}