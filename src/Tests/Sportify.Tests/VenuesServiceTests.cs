using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sportify.AutoMapper;
using Sportify.Data;
using Sportify.Data.ViewModels.Venues;
using Sportify.Services;
using System.Linq;
using Xunit;

namespace Sportify.Services.Tests
{
    public class VenuesServiceTests
    {
        [Fact]
        public void AddVenueShouldReturnsorrectCountUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<SportifyDbContext>()
                .UseInMemoryDatabase("Sportify_Database_Venues_1")
                .Options;

            var context = new SportifyDbContext(options);
            var mapperConfig = new MapperConfiguration(m => m.AddProfile(new MapperProfile()));
            var mapper = mapperConfig.CreateMapper();

            var service = new VenuesService(context, mapper, null, null);
            service.AddVenue(new AddVenueViewModel());

            var count = context.Venues.Count();

            Assert.Equal(1, count);
        }
    }
}
