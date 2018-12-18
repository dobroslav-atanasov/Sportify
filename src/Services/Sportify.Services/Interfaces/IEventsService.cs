namespace Sportify.Services.Interfaces
{
    using System.Collections.Generic;
    using Data.Models;
    using Data.ViewModels.Countries;
    using Data.ViewModels.Events;

    public interface IEventsService
    {
        Event Create(CreateEventViewModel model);

        IEnumerable<EventViewModel> GetAllEvents();

        IEnumerable<EventViewModel> GetAllEventsInCountry(SearchCountryViewModel model);

        EventViewModel GetEventById(int id);

        void JoinUserToEvent(string userId, int eventId);

        bool IsUserParticipate(string userId, int eventId);
    }
}