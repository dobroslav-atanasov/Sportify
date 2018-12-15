using Sportify.Data.Models;
using Sportify.Data.ViewModels.Events;

namespace Sportify.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Linq;
    using Data.ViewModels.Countries;

    public interface IEventsService
    {
        Event Create(CreateEventViewModel model);

        IEnumerable<EventViewModel> GetAllEvents();

        IEnumerable<EventViewModel> GetAllEventsInCountry(SearchCountryViewModel model);
    }
}