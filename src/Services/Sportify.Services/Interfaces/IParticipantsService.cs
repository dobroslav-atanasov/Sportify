namespace Sportify.Services.Interfaces
{
    using System.Collections.Generic;

    using Data.ViewModels.Events;
    using Data.ViewModels.Participants;
    using Data.ViewModels.Results;

    public interface IParticipantsService
    {
        IList<ParticipantViewModel> GetParticipantsInEventId(int id);

        IList<ParticipantViewModel> SetResults(int eventId, IList<ParticipantViewModel> models);

        IEnumerable<MyResultViewModel> GetResultByUser(string username);

        IEnumerable<MyEventViewModel> GetEventsWithMyParticipation(string username);
    }
}