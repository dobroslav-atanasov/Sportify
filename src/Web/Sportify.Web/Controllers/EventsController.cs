namespace Sportify.Web.Controllers
{
    using Constants;
    using Data.ViewModels.Countries;
    using Data.ViewModels.Events;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.Interfaces;

    public class EventsController : Controller
    {
        private readonly IOrganizationsService organizationsService;
        private readonly IDisciplinesService disciplinesService;
        private readonly IVenuesService venuesService;
        private readonly IEventsService eventsService;
        private readonly ICountriesService countriesService;

        public EventsController(IOrganizationsService organizationsService, IDisciplinesService disciplinesService, IVenuesService venuesService, IEventsService eventsService, ICountriesService countriesService)
        {
            this.organizationsService = organizationsService;
            this.disciplinesService = disciplinesService;
            this.venuesService = venuesService;
            this.eventsService = eventsService;
            this.countriesService = countriesService;
        }

        [Authorize(Roles = Constants.EditorRole)]
        public IActionResult Create()
        {
            this.ViewData["Organizations"] = this.organizationsService.GetAllOrganizations();
            this.ViewData["Disciplines"] = this.disciplinesService.GetAllDisciplines();
            this.ViewData["Venues"] = this.venuesService.GetAllVenues();
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = Constants.EditorRole)]
        public IActionResult Create(CreateEventViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            this.eventsService.Create(model);
            return this.RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = Constants.AdminAndEditorRoles)]
        public IActionResult All()
        {
            var events = this.eventsService.GetAllEvents();
            return this.View(events);
        }

        [HttpPost]
        public IActionResult SearchEvents(SearchCountryViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.ViewData["Countries"] = this.countriesService.GetAllCountryNames();
                return this.RedirectToAction("Index", "Home");
            }

            this.ViewData["Country"] = this.countriesService.GetCountryById(model.CountryId).Name;
            var events = this.eventsService.GetAllEventsInCountry(model);
            
            return this.View(events);
        }

        [Authorize]
        public IActionResult Info(int id)
        {
            var @event = this.eventsService.GetEventById(id);

            return this.View(@event);
        }

        public IActionResult Participate(int id)
        {
            var asd = 1;
            return this.RedirectToAction("Info", "Events", new { id = id });
        }
    }
}