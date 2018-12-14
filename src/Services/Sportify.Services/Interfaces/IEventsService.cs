using Sportify.Data.Models;
using Sportify.Data.ViewModels.Events;

namespace Sportify.Services.Interfaces
{
    public interface IEventsService
    {
        Event Create(CreateEventViewModel model);
    }
}