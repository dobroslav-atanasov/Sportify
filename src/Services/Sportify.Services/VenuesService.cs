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

        public void AddVenue(AddVenueViewModel model)
        {
            var venue = this.Mapper.Map<Venue>(model);

            this.Context.Venues.Add(venue);
            this.Context.SaveChanges();
        }
    }
}