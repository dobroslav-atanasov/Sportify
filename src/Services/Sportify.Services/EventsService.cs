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

        public Participant JoinUserToEvent(string userId, int eventId)
        {
            var participant = new Participant
            {
                UserId = userId,
                EventId = eventId
            };

            this.Context.Participants.Add(participant);
            this.Context.SaveChanges();

            return participant;
        }

        public void LeaveUserFromEvent(string userId, int eventId)
        {
            var participant = this.Context
                .Participants
                .FirstOrDefault(p => p.UserId == userId && p.EventId == eventId);

            this.Context.Remove(participant);
            this.Context.SaveChanges();
        }

        public IEnumerable<EventViewModel> GetEventsByUser(string username)
        {
            var events = this.Context
                .Events
                .Where(e => e.Organization.President.UserName == username)
                .OrderBy(e => e.Date)
                .AsQueryable();

            var eventViewModels = this.Mapper.Map<IQueryable<Event>, IEnumerable<EventViewModel>>(events);

            return eventViewModels;
        }

        public UpdateEventViewModel GetEventForUpdateById(int id)
        {
            var @event = this.Context
                .Events
                .FirstOrDefault(e => e.Id == id);

            var eventViewModel = this.Mapper.Map<UpdateEventViewModel>(@event);

            return eventViewModel;
        }

        public UpdateEventViewModel UpdateEvent(UpdateEventViewModel model)
        {
            var @event = this.Context
                .Events
                .FirstOrDefault(e => e.Id == model.Id);

            if (@event == null)
            {
                return null;
            }

            @event.EventName = model.EventName;
            @event.Date = model.Date;
            @event.OrganizationId = model.OrganizationId;
            @event.DisciplineId = model.DisciplineId;
            @event.VenueId = model.VenueId;
            @event.NumberOfParticipants = model.NumberOfParticipants;
            this.Context.SaveChanges();

            var eventViewModel = this.Mapper.Map<UpdateEventViewModel>(@event);

            return eventViewModel;
        }

        public void DeleteEvent(EventViewModel model)
        {
            var @event = this.Context
                .Events
                .FirstOrDefault(d => d.Id == model.Id);

            if (@event != null)
            {
                this.Context.Events.Remove(@event);
                this.Context.SaveChanges();
            }
        }

        public bool CheckForFreeSpace(int eventId)
        {
            var @event = this.Context
                .Events
                .FirstOrDefault(e => e.Id == eventId);

            var participants = this.Context
                .Participants
                .Where(e => e.EventId == eventId)
                .Count();

            return @event.NumberOfParticipants > participants;
        }
    }
}