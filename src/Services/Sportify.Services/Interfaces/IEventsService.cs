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
        
        bool IsUserParticipate(string userId, int eventId);

        Participant JoinUserToEvent(string userId, int eventId);

        void LeaveUserFromEvent(string userId, int eventId);

        IEnumerable<EventViewModel> GetEventsByUser(string username);

        UpdateEventViewModel GetEventForUpdateById(int id);

        UpdateEventViewModel UpdateEvent(UpdateEventViewModel model);

        void DeleteEvent(EventViewModel model);

        bool CheckForFreeSpace(int eventId);
    }
}