namespace Sportify.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.Interfaces;

    public class EventsController : Controller
    {
        private readonly IOrganizationsService organizationsService;
        private readonly IDisciplinesService disciplinesService;
        private readonly IVenuesService venuesService;

        public EventsController(IOrganizationsService organizationsService, IDisciplinesService disciplinesService, IVenuesService venuesService)
        {
            this.organizationsService = organizationsService;
            this.disciplinesService = disciplinesService;
            this.venuesService = venuesService;
        }

        [Authorize(Roles = Constants.Constants.EditorRole)]
        public IActionResult Create()
        {
            this.ViewData["Organizations"] = this.organizationsService.GetAllOrganizations();
            this.ViewData["Disciplines"] = this.disciplinesService.GetAllDisciplines();
            this.ViewData["Venues"] = this.venuesService.GetAllVenues();
            return this.View();
        }
    }
}