namespace Sportify.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Data;
    using Data.Models;
    using Data.ViewModels.Participants;
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

        public IList<ParticipantViewModel> SetUpResults(int eventId, IList<ParticipantViewModel> models)
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
    }
}