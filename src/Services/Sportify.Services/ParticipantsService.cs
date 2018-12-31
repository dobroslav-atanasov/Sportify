namespace Sportify.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Data;
    using Data.Models;
    using Data.ViewModels.Events;
    using Data.ViewModels.Participants;
    using Data.ViewModels.Results;
    using global::AutoMapper;
    using Interfaces;
    using Microsoft.AspNetCore.Identity;

    public class ParticipantsService : BaseService, IParticipantsService
    {
        public ParticipantsService(SportifyDbContext context, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager) 
            : base(context, mapper, userManager, signInManager)
        {
        }

        public IList<ParticipantViewModel> GetParticipantsInEventId(int id)
        {
            var users = this.Context
                .Participants
                .Where(u => u.EventId == id)
                .AsQueryable();

            var participants = this.Mapper.Map<IQueryable<Participant>, IList<ParticipantViewModel>>(users);

            return participants;
        }

        public IList<ParticipantViewModel> SetResults(int eventId, IList<ParticipantViewModel> models)
        {
            foreach (var model in models)
            {
                var participant = this.Context
                    .Participants
                    .FirstOrDefault(p => p.UserId == model.UserId && p.EventId == model.EventId);

                if (participant != null)
                {
                    participant.Result = model.Result;
                }

                this.Context.SaveChanges();
            }

            var participants = this.GetParticipantsInEventId(eventId);

            return participants;
        }

        public IEnumerable<MyResultViewModel> GetResultByUser(string username)
        {
            var participant = this.Context
                .Participants
                .Where(p => p.User.UserName == username && p.Result != null)
                .OrderBy(p => p.Event.Date)
                .AsQueryable();

            var results = this.Mapper.Map<IQueryable<Participant>, IEnumerable<MyResultViewModel>>(participant);

            return results;
        }

        public IEnumerable<MyEventViewModel> GetEventsWithMyParticipation(string username)
        {
            var participant = this.Context
                .Participants
                .Where(p => p.User.UserName == username && p.Event.Date >= DateTime.UtcNow)
                .OrderBy(p => p.Event.Date)
                .AsQueryable();

            var events = this.Mapper.Map<IQueryable<Participant>, IEnumerable<MyEventViewModel>>(participant);

            return events;
        }

        public IEnumerable<EventResultViewModel> GetEventResultsByEventId(int id)
        {
            var participant = this.Context
                .Participants
                .Where(p => p.EventId == id)
                .OrderBy(p => p.Result)
                .AsQueryable();

            var results = this.Mapper.Map<IQueryable<Participant>, IEnumerable<EventResultViewModel>>(participant);

            return results;
        }
    }
}