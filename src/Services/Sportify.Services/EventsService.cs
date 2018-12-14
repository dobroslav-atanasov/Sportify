namespace Sportify.Services
{
    using global::AutoMapper;
    using Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Sportify.Data;
    using Sportify.Data.Models;
    using Sportify.Data.ViewModels.Events;
    using System.Linq;

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
    }
}