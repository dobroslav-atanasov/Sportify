namespace Sportify.Tests
{
    using System.Linq;
    using AutoMapper;
    using Data;
    using Data.Models;
    using Data.ViewModels.Venues;
    using global::AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Services;
    using Xunit;

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

        [Fact]
        public void GetAllVenuesShouldReturnCorrectCountUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<SportifyDbContext>()
                .UseInMemoryDatabase("Sportify_Database_Venues_2")
                .Options;

            var context = new SportifyDbContext(options);
            var mapperConfig = new MapperConfiguration(m => m.AddProfile(new MapperProfile()));
            var mapper = mapperConfig.CreateMapper();

            context.Venues.Add(new Venue());
            context.Venues.Add(new Venue());
            context.SaveChanges();

            var service = new VenuesService(context, mapper, null, null);

            var count = service.GetAllVenues().Count();
            Assert.Equal(2, count);
        }
    }
}