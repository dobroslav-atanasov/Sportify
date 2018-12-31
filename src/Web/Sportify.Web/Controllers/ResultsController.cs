namespace Sportify.Web.Controllers
{
    using System.Collections.Generic;

    using Constants;
    using Data.Models;
    using Data.ViewModels.Participants;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services.Interfaces;

    public class ResultsController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IParticipantsService participantsService;
        private readonly IEventsService eventsService;

        public ResultsController(UserManager<User> userManager, IParticipantsService participantsService, IEventsService eventsService)
        {
            this.userManager = userManager;
            this.participantsService = participantsService;
            this.eventsService = eventsService;
        }

        [Authorize(Roles = Role.Editor)]
        public IActionResult Results(int id)
        {
            var @event = this.eventsService.GetEventById(id);
            if (@event == null)
            {
                this.TempData[GlobalConstants.Message] = GlobalConstants.EventDoesNotExist;
                return this.RedirectToAction("Invalid", "Home", new { AreaConstants.Base });
            }

            this.ViewData[GlobalConstants.Event] = @event.EventName;
            this.ViewData[GlobalConstants.Discipline] = @event.Discipline;
            var participants = this.participantsService.GetParticipantsInEventId(id);
            return this.View(participants);
        }

        [HttpPost]
        [Authorize(Roles = Role.Editor)]
        public IActionResult Results(int id, IList<ParticipantViewModel> models)
        {
            var @event = this.eventsService.GetEventById(id);
            this.ViewData[GlobalConstants.Event] = @event.EventName;

            if (!this.ModelState.IsValid)
            {
                return this.View(models);
            }

            var participants = this.participantsService.SetResults(id, models);
            this.ViewData[GlobalConstants.Message] = GlobalConstants.ResultsWereUpdated;
            return this.View(participants);
        }

        [Authorize]
        public IActionResult MyResults()
        {
            var user = this.userManager.FindByNameAsync(this.User.Identity.Name).GetAwaiter().GetResult();
            var results = this.participantsService.GetResultByUser(user.UserName);
            return this.View(results);
        }

        public IActionResult EventResults(int id)
        {
            var results = this.participantsService.GetEventResultsByEventId(id);
            return this.View(results);
        }
    }
}