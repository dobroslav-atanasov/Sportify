namespace Sportify.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Data.Models;
    using Data.ViewModels.Venues;
    using global::AutoMapper;
    using Interfaces;
    using Microsoft.AspNetCore.Identity;

    public class VenuesService : BaseService, IVenuesService
    {
        public VenuesService(SportifyDbContext context, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
            : base(context, mapper, userManager, signInManager)
        {
        }

        public IEnumerable<VenueViewModel> GetAllVenues()
        {
            var venues = this.Context
                .Venues
                .OrderBy(v => v.Name)
                .AsQueryable();

            var venuesViewModel = this.Mapper.Map<IQueryable<Venue>, IEnumerable<VenueViewModel>>(venues);
            return venuesViewModel;
        }

        public Venue AddVenue(AddVenueViewModel model)
        {
            var venue = this.Mapper.Map<Venue>(model);

            this.Context.Venues.Add(venue);
            this.Context.SaveChanges();

            return venue;
        }

        public VenueViewModel GetVenueById(int id)
        {
            var venue = this.Context
                .Venues
                .FirstOrDefault(v => v.Id == id);

            var venueViewModel = this.Mapper.Map<VenueViewModel>(venue);

            return venueViewModel;
        }

        public VenueViewModel UpdateVenue(VenueViewModel model)
        {
            var venue = this.Context
                .Venues
                .FirstOrDefault(s => s.Id == model.Id);

            if (venue == null)
            {
                return null;
            }

            venue.Name = model.Name;
            venue.Address = model.Address;
            venue.Capacity = model.Capacity;
            venue.ImageVenueUrl = model.ImageVenueUrl;
            venue.TownId = model.TownId;
            this.Context.SaveChanges();

            var disciplineViewModel = this.Mapper.Map<VenueViewModel>(venue);

            return disciplineViewModel;
        }

        public void DeleteVenue(VenueViewModel model)
        {
            var venue = this.Context
                .Venues
                .FirstOrDefault(d => d.Id == model.Id);

            if (venue != null)
            {
                this.Context.Venues.Remove(venue);
                this.Context.SaveChanges();
            }
        }
    }
}