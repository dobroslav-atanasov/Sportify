namespace Sportify.Services.Interfaces
{
    using System.Collections.Generic;

    using Data.Models;
    using Data.ViewModels.Venues;

    public interface IVenuesService
    {
        IEnumerable<VenueViewModel> GetAllVenues();

        Venue AddVenue(AddVenueViewModel model);

        VenueViewModel GetVenueById(int id);

        VenueViewModel UpdateVenue(VenueViewModel model);

        void DeleteVenue(VenueViewModel model);

        IEnumerable<VenueViewModel> GetAllVenuesByCountryId(int countryId);
    }
}