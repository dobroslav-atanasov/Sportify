namespace Sportify.Tests
{
    using System.Linq;
    using AutoMapper;
    using Data;
    using Data.Models;
    using Data.ViewModels.Venues;
    using global::AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Services;
    using Sportify.Data.ViewModels.Disciplines;
    using Xunit;

    public class VenuesServiceTests : BaseServiceTests
    {
        [Fact]
        public void AddVenue_ShouldReturnCorrectVenue()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var service = new VenuesService(context, this.Mapper, null, null);

            // Act
            var venue = service.AddVenue(new AddVenueViewModel
            {
                Name = "Test",
                Address = "Test Address",
                Capacity = 1000,
                ImageVenueUrl = "www.test.com",
                TownId = 1
            });

            // Expected Venue
            var expectedVenue = new Venue
            {
                Id = 1,
                Name = "Test",
                Address = "Test Address",
                Capacity = 1000,
                ImageVenueUrl = "www.test.com",
                TownId = 1
            };

            // Assert
            Assert.True(venue.Equals(expectedVenue));
        }

        [Fact]
        public void GetAllVenues_ShouldReturnCorrectCount()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var service = new VenuesService(context, this.Mapper, null, null);
            context.Add(new Venue());
            context.Add(new Venue());
            context.SaveChanges();

            // Act
            var result = service.GetAllVenues().Count();

            // Assert
            Assert.Equal(2, result);
        }
    }
}