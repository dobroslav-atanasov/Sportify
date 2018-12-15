using Sportify.Data.Models;
using Sportify.Data.ViewModels.Events;

namespace Sportify.Services.Interfaces
{
    using System.Collections.Generic;

    public interface IEventsService
    {
        Event Create(CreateEventViewModel model);

        IEnumerable<EventViewModel> GetAllEvents();
    }
}