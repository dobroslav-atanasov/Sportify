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
        public void AddVenue_ShouldReturnsorrectCount()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var service = new VenuesService(context, this.Mapper, null, null);

            // Act
            service.AddVenue(new AddVenueViewModel());
            var result = context.Venues.Count();

            // Assert
            Assert.Equal(1, result);
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