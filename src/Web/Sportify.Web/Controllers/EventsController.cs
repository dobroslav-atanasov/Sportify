﻿namespace Sportify.Web.Controllers
{
    using Constants;
    using Data.Models;
    using Data.ViewModels.Countries;
    using Data.ViewModels.Events;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services.Interfaces;
    using X.PagedList;

    public class EventsController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IOrganizationsService organizationsService;
        private readonly IDisciplinesService disciplinesService;
        private readonly IVenuesService venuesService;
        private readonly IEventsService eventsService;
        private readonly ICountriesService countriesService;
        private readonly IParticipantsService participantsService;

        public EventsController(UserManager<User> userManager, IOrganizationsService organizationsService, IDisciplinesService disciplinesService,
            IVenuesService venuesService, IEventsService eventsService, ICountriesService countriesService, IParticipantsService participantsService)
        {
            this.userManager = userManager;
            this.organizationsService = organizationsService;
            this.disciplinesService = disciplinesService;
            this.venuesService = venuesService;
            this.eventsService = eventsService;
            this.countriesService = countriesService;
            this.participantsService = participantsService;
        }

        [Authorize(Roles = Role.Editor)]
        public IActionResult Create()
        {
            this.ViewData[GlobalConstants.Organizations] = this.organizationsService.GetAllOrganizations();
            this.ViewData[GlobalConstants.Disciplines] = this.disciplinesService.GetAllDisciplines();
            this.ViewData[GlobalConstants.Venues] = this.venuesService.GetAllVenues();
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = Role.Editor)]
        public IActionResult Create(CreateEventViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            this.eventsService.Create(model);
            return this.RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = Role.AdministratorAndEditor)]
        public IActionResult All(int? page)
        {
            var events = this.eventsService.GetAllEvents();

            var pageNumber = page ?? 1;
            var eventsOnPage = events.ToPagedList(pageNumber, 10);

            return this.View(eventsOnPage);
        }

        [HttpPost]
        public IActionResult EventsInCountry(SearchCountryViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.ViewData[GlobalConstants.Countries] = this.countriesService.GetAllCountryNames();
                return this.RedirectToAction("Index", "Home");
            }

            this.ViewData[GlobalConstants.Country] = this.countriesService.GetCountryById(model.CountryId).Name;
            var events = this.eventsService.GetAllEventsInCountry(model);

            return this.View(events);
        }

        [Authorize]
        public IActionResult UpcomingEvent(int id)
        {
            var user = this.userManager.FindByNameAsync(this.User.Identity.Name).GetAwaiter().GetResult();
            var @event = this.eventsService.GetEventById(id);
            var isUserParticipate = this.eventsService.IsUserParticipate(user.Id, @event.Id);
            var hasFreeSeats = this.eventsService.CheckForFreeSpace(id);

            if (!hasFreeSeats)
            {
                this.ViewData[GlobalConstants.Error] = GlobalConstants.NoFreeSeats;
            }

            if (isUserParticipate)
            {
                this.ViewData[GlobalConstants.Participate] = true;
            }
            else
            {
                this.ViewData[GlobalConstants.Participate] = false;
            }

            return this.View(@event);
        }

        [Authorize]
        public IActionResult Join(int id)
        {
            var user = this.userManager.FindByNameAsync(this.User.Identity.Name).GetAwaiter().GetResult();
            var @event = this.eventsService.GetEventById(id);

            if (user == null || @event == null)
            {
                return this.RedirectToAction("Index", "Home", new { area = AreaConstants.Base });
            }

            this.eventsService.JoinUserToEvent(user.Id, @event.Id);
            return this.RedirectToAction("UpcomingEvent", "Events", new { id = id });
        }

        [Authorize]
        public IActionResult Leave(int id)
        {
            var user = this.userManager.FindByNameAsync(this.User.Identity.Name).GetAwaiter().GetResult();
            var @event = this.eventsService.GetEventById(id);

            this.eventsService.LeaveUserFromEvent(user.Id, @event.Id);
            return this.RedirectToAction("UpcomingEvent", "Events", new { id = id });
        }

        [Authorize(Roles = Role.Editor)]
        public IActionResult Events(int? page)
        {
            var events = this.eventsService.GetEventsByUser(this.User.Identity.Name);

            var pageNumber = page ?? 1;
            var eventsOnPage = events.ToPagedList(pageNumber, 10);

            return this.View(eventsOnPage);
        }

        [Authorize(Roles = Role.Editor)]
        public IActionResult Edit(int id)
        {
            this.ViewData[GlobalConstants.Organizations] = this.organizationsService.GetAllOrganizations();
            this.ViewData[GlobalConstants.Disciplines] = this.disciplinesService.GetAllDisciplines();
            this.ViewData[GlobalConstants.Venues] = this.venuesService.GetAllVenues();
            var @event = this.eventsService.GetEventForUpdateById(id);
            if (@event == null)
            {
                this.TempData[GlobalConstants.Message] = GlobalConstants.EventDoesNotExist;
                return this.RedirectToAction("Invalid", "Home", new { AreaConstants.Base });
            }

            return this.View(@event);
        }

        [HttpPost]
        [Authorize(Roles = Role.Editor)]
        public IActionResult Edit(UpdateEventViewModel model)
        {
            this.ViewData[GlobalConstants.Organizations] = this.organizationsService.GetAllOrganizations();
            this.ViewData[GlobalConstants.Disciplines] = this.disciplinesService.GetAllDisciplines();
            this.ViewData[GlobalConstants.Venues] = this.venuesService.GetAllVenues();

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var @event = this.eventsService.UpdateEvent(model);
            if (@event == null)
            {
                this.ViewData[GlobalConstants.Error] = GlobalConstants.EventWasNotUpdated;
                return this.View(model);
            }

            this.ViewData[GlobalConstants.Message] = GlobalConstants.EventWasUpdated;
            return this.View();
        }

        [Authorize(Roles = Role.Editor)]
        public IActionResult Details(int id)
        {
            var @event = this.eventsService.GetEventById(id);
            if (@event == null)
            {
                this.TempData[GlobalConstants.Message] = GlobalConstants.EventDoesNotExist;
                return this.RedirectToAction("Invalid", "Home", new { AreaConstants.Base });
            }
            return this.View(@event);
        }

        [Authorize(Roles = Role.Editor)]
        public IActionResult Delete(int id)
        {
            var @event = this.eventsService.GetEventById(id);
            if (@event == null)
            {
                this.TempData[GlobalConstants.Message] = GlobalConstants.EventDoesNotExist;
                return this.RedirectToAction("Invalid", "Home", new { area = AreaConstants.Base });
            }
            return this.View(@event);
        }

        [HttpPost]
        [Authorize(Roles = Role.Editor)]
        public IActionResult Delete(EventViewModel model)
        {
            this.eventsService.DeleteEvent(model);
            return this.RedirectToAction("Events", "Events", new { area = AreaConstants.Base });
        }

        [Authorize]
        public IActionResult MyEvents()
        {
            var user = this.userManager.FindByNameAsync(this.User.Identity.Name).GetAwaiter().GetResult();
            var events = this.participantsService.GetEventsWithMyParticipation(user.UserName);
            return this.View(events);
        }
    }
}