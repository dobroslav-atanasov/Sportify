namespace Sportify.Web.Controllers
{
    using System.Collections.Generic;
    using Constants;
    using Data.ViewModels.Countries;
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
        [Authorize]
        public IActionResult SearchEvents(SearchCountryViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.ViewData["Countries"] = this.countriesService.GetAllCountryNames();
                return this.RedirectToAction("Index", "Home");
            }
            var events = this.eventsService.GetAllEventsInCountry(model);
            
            return this.View(events);
        }
    }
}