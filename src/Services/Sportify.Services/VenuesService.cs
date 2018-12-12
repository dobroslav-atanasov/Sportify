using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Sportify.Data;
using Sportify.Data.Models;
using Sportify.Data.ViewModels.Venues;
using Sportify.Services.Interfaces;

namespace Sportify.Services
{
    public class VenuesService : BaseService, IVenuesService
    {
        public VenuesService(SportifyDbContext context, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
            :base(context, mapper, userManager, signInManager)
        {
        }

        public void AddVenue(AddVenueViewModel model)
        {
            var venue = this.Mapper.Map<Venue>(model);

            this.Context.Venues.Add(venue);
            this.Context.SaveChanges();
        }
    }
}
