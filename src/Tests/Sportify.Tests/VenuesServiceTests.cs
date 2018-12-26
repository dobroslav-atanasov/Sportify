namespace Sportify.Tests
{
    using System.Linq;
    using Data;
    using Data.Models;
    using Data.ViewModels.Venues;
    using Microsoft.Extensions.DependencyInjection;
    using Services;
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

        [Fact]
        public void GetVenueById_ShouldReturnCorrectVenueViewModel()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var service = new VenuesService(context, this.Mapper, null, null);

            service.AddVenue(new AddVenueViewModel()
            {
                Name = "Venue Test",
                Capacity = 5000,
                ImageVenueUrl = "Venue Image Url",
                TownId = 1
            });

            // Act
            var venue = service.GetVenueById(1);

            // Expected Sport
            var expectedVenue = new VenueViewModel()
            {
                Id = 1,
                Name = "Venue Test",
                Capacity = 5000,
                ImageVenueUrl = "Venue Image Url",
                TownId = 1
            };

            // Assert
            Assert.True(venue.Equals(expectedVenue));
        }

        [Fact]
        public void UpdateVenue_ShouldReturnCorrectVenueViewModel()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var service = new VenuesService(context, this.Mapper, null, null);

            service.AddVenue(new AddVenueViewModel()
            {
                Name = "Venue Test",
                Capacity = 5000,
                ImageVenueUrl = "Venue Image Url",
                TownId = 1
            });

            // Act
            var venue = service.UpdateVenue(new VenueViewModel()
            {
                Id = 1,
                Name = "New Venue Name",
                Capacity = 10000,
                ImageVenueUrl = "New Venue Image Url",
                TownId = 3
            });

            // Expected Venue
            var expectedVenue = new VenueViewModel()
            {
                Id = 1,
                Name = "New Venue Name",
                Capacity = 10000,
                ImageVenueUrl = "New Venue Image Url",
                TownId = 3
            };

            // Assert
            Assert.True(venue.Equals(expectedVenue));
        }

        [Fact]
        public void UpdateVenue_ShouldReturnNull()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var service = new VenuesService(context, this.Mapper, null, null);

            service.AddVenue(new AddVenueViewModel()
            {
                Name = "Venue Test",
                Capacity = 5000,
                ImageVenueUrl = "Venue Image Url",
                TownId = 1
            });

            // Act
            var venue = service.UpdateVenue(new VenueViewModel()
            {
                Name = "Venue Test",
                Capacity = 5000,
                ImageVenueUrl = "Venue Image Url",
                TownId = 1
            });

            // Assert
            Assert.Null(venue);
        }

        [Fact]
        public void DeleteVenue_ShouldReturnCorrectCount()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var service = new VenuesService(context, this.Mapper, null, null);

            service.AddVenue(new AddVenueViewModel()
            {
                Name = "Venue Test",
                Capacity = 5000,
                ImageVenueUrl = "Venue Image Url",
                TownId = 1
            });

            service.AddVenue(new AddVenueViewModel()
            {
                Name = "Second Venue Test",
                Capacity = 1000,
                ImageVenueUrl = "Venue Image Url",
                TownId = 2
            });

            service.AddVenue(new AddVenueViewModel()
            {
                Name = "Third Venue Test",
                Capacity = 200,
                ImageVenueUrl = "Venue Image Url",
                TownId = 1
            });

            // Act
            service.DeleteVenue(new VenueViewModel() { Id = 1 });
            var result = context.Venues.Count();

            // Assert
            Assert.Equal(2, result);
        }

        [Fact]
        public void GetAllVenuesByCountryId_ShouldReturnCorrectCount()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var service = new VenuesService(context, this.Mapper, null, null);
            context.Towns.Add(new Town { CountryId = 1 });
            context.SaveChanges();

            service.AddVenue(new AddVenueViewModel()
            {
                Name = "Venue Test",
                Capacity = 5000,
                ImageVenueUrl = "Venue Image Url",
                TownId = 1
            });

            service.AddVenue(new AddVenueViewModel()
            {
                Name = "Second Venue Test",
                Capacity = 1000,
                ImageVenueUrl = "Venue Image Url",
                TownId = 2
            });

            service.AddVenue(new AddVenueViewModel()
            {
                Name = "Third Venue Test",
                Capacity = 200,
                ImageVenueUrl = "Venue Image Url",
                TownId = 1
            });

            // Act
            var result = service.GetAllVenuesByCountryId(1).Count();

            // Assert
            Assert.Equal(2, result);
        }
    }
}