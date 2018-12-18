namespace Sportify.Web.Controllers
{
    using Constants;
    using Data.ViewModels.Countries;
    using Data.ViewModels.Events;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services.Interfaces;
    using Sportify.Data.Models;

    public class EventsController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IOrganizationsService organizationsService;
        private readonly IDisciplinesService disciplinesService;
        private readonly IVenuesService venuesService;
        private readonly IEventsService eventsService;
        private readonly ICountriesService countriesService;

        public EventsController(UserManager<User> userManager, SignInManager<User> signInManager, IOrganizationsService organizationsService, IDisciplinesService disciplinesService, 
            IVenuesService venuesService, IEventsService eventsService, ICountriesService countriesService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
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
            var user = this.userManager.FindByNameAsync(this.User.Identity.Name).GetAwaiter().GetResult();
            var @event = this.eventsService.GetEventById(id);
            var isUserParticipate = this.eventsService.IsUserParticipate(user.Id, @event.Id);

            if (isUserParticipate)
            {
                this.ViewData["Participate"] = true;
            }
            else
            {
                this.ViewData["Participate"] = false;
            }

            return this.View(@event);
        }

        [Authorize]
        public IActionResult Join(int id)
        {
            var user = this.userManager.FindByNameAsync(this.User.Identity.Name).GetAwaiter().GetResult();
            var @event = this.eventsService.GetEventById(id);

            this.eventsService.JoinUserToEvent(user.Id, @event.Id);
            return this.RedirectToAction("Info", "Events", new { id = id });
        }
    }
}