namespace Sportify.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.Interfaces;
    using Sportify.Data.ViewModels.Events;

    public class EventsController : Controller
    {
        private readonly IOrganizationsService organizationsService;
        private readonly IDisciplinesService disciplinesService;
        private readonly IVenuesService venuesService;
        private readonly IEventsService eventsService;

        public EventsController(IOrganizationsService organizationsService, IDisciplinesService disciplinesService, IVenuesService venuesService, IEventsService eventsService)
        {
            this.organizationsService = organizationsService;
            this.disciplinesService = disciplinesService;
            this.venuesService = venuesService;
            this.eventsService = eventsService;
        }

        [Authorize(Roles = Constants.Constants.EditorRole)]
        public IActionResult Create()
        {
            this.ViewData["Organizations"] = this.organizationsService.GetAllOrganizations();
            this.ViewData["Disciplines"] = this.disciplinesService.GetAllDisciplines();
            this.ViewData["Venues"] = this.venuesService.GetAllVenues();
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = Constants.Constants.EditorRole)]
        public IActionResult Create(CreateEventViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            this.eventsService.Create(model);
            return this.RedirectToAction("Index", "Home");
        }
    }
}