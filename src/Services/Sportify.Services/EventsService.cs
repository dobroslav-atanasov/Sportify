namespace Sportify.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Data.Models;
    using Data.ViewModels.Events;
    using global::AutoMapper;
    using Interfaces;
    using Microsoft.AspNetCore.Identity;

    public class EventsService : BaseService, IEventsService
    {
        public EventsService(SportifyDbContext context, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager) 
            : base(context, mapper, userManager, signInManager)
        {
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
    }
}