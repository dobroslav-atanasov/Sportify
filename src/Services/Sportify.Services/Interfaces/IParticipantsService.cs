namespace Sportify.Services.Interfaces
{
    using System.Collections.Generic;

    using Data.ViewModels.Participants;

    public interface IParticipantsService
    {
        IList<ParticipantViewModel> GetParticipantsInEventId(int id);

        IList<ParticipantViewModel> SetUpResults(int eventId, IList<ParticipantViewModel> models);
    }
}