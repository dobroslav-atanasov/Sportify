using Sportify.Data.ViewModels.Venues;

namespace Sportify.Services.Interfaces
{
    using Sportify.Data.Models;
    using System.Collections.Generic;

    public interface IVenuesService
    {
        IEnumerable<VenueViewModel> GetAllVenues();

        Venue AddVenue(AddVenueViewModel model);
    }
}
