using Sportify.Data.ViewModels.Venues;

namespace Sportify.Services.Interfaces
{
    using System.Collections.Generic;

    public interface IVenuesService
    {
        IEnumerable<VenueViewModel> GetAllVenues();

        void AddVenue(AddVenueViewModel model);
    }
}
