namespace Sportify.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Data.Models;
    using Data.ViewModels.Countries;
    using Data.ViewModels.Events;
    using global::AutoMapper;
    using Interfaces;
    using Microsoft.AspNetCore.Identity;

    public class EventsService : BaseService, IEventsService
    {
        private readonly ITownsService townsService;

        public EventsService(SportifyDbContext context, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, ITownsService townsService) 
            : base(context, mapper, userManager, signInManager)
        {
            this.townsService = townsService;
        }

        public Event Create(CreateEventViewModel model)
        {
            var @event = this.Mapper.Map<Event>(model);

            this.Context.Events.Add(@event);
            this.Context.SaveChanges();

            return @event;
        }

        public IEnumerable<EventViewModel> GetAllEvents()
        {
            var events = this.Context
                .Events
                .OrderBy(e => e.Date)
                .AsQueryable();

            var eventsViewModel = this.Mapper.Map<IQueryable<Event>, IEnumerable<EventViewModel>>(events);

            return eventsViewModel;
        }

        public IEnumerable<EventViewModel> GetAllEventsInCountry(SearchCountryViewModel model)
        {
            var townIds = this.townsService.GetAllTownIdsByCountryId(model.CountryId);

            var events = this.Context
                .Events
                .Where(e => townIds.Contains(e.Venue.TownId))
                .OrderBy(e => e.Date);

            var eventsViewModel = this.Mapper.Map<IQueryable<Event>, IEnumerable<EventViewModel>>(events);

            return eventsViewModel;
        }

        public EventViewModel GetEventById(int id)
        {
            var @event = this.Context
                .Events
                .FirstOrDefault(e => e.Id == id);

            var eventViewModel = this.Mapper.Map<EventViewModel>(@event);

            return eventViewModel;
        }

        public bool IsUserParticipate(string userId, int eventId)
        {
            var isUserParticipate = this.Context.Participants.Any(p => p.UserId == userId && p.EventId == eventId);

            return isUserParticipate;
        }

        public void JoinUserToEvent(string userId, int eventId)
        {
            var participant = new Participant
            {
                UserId = userId,
                EventId = eventId
            };

            this.Context.Participants.Add(participant);
            this.Context.SaveChanges();
        }

        public void LeaveUserFromEvent(string userId, int eventId)
        {
            var participant = this.Context
                .Participants
                .FirstOrDefault(p => p.UserId == userId && p.EventId == eventId);

            this.Context.Remove(participant);
            this.Context.SaveChanges();
        }
    }
}