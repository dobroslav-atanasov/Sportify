namespace Sportify.Tests
{
    using System;
    using System.Globalization;
    using System.Linq;
    using Data;
    using Data.Models;
    using Data.ViewModels.Countries;
    using Data.ViewModels.Events;
    using Microsoft.Extensions.DependencyInjection;
    using Services;
    using Xunit;

    public class EventsServiceTests : BaseServiceTests
    {
        [Fact]
        public void CreateEventShould_ReturnCorrectObject()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var service = new EventsService(context, this.Mapper, null, null, null);

            // Act
            var @event = service.Create(new CreateEventViewModel
            {
                EventName = "Test",
                Date = DateTime.ParseExact("20-12-2018 10:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                OrganizationId = 1,
                DisciplineId = 1,
                VenueId = 1,
                NumberOfParticipants = 10
            });

            // Expected Event
            var expectedEvent = new Event
            {
                Id = 1,
                EventName = "Test",
                Date = DateTime.ParseExact("20-12-2018 10:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                OrganizationId = 1,
                DisciplineId = 1,
                VenueId = 1,
                NumberOfParticipants = 10
            };

            // Assert
            Assert.True(@event.Equals(expectedEvent));
        }

        [Fact]
        public void GetGetAllEvents_ShouldReturnCorrectCount()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var service = new EventsService(context, this.Mapper, null, null, null);
            context.Add(new Event());
            context.Add(new Event());
            context.Add(new Event());
            context.SaveChanges();

            // Act
            var result = service.GetAllEvents().Count();

            // Assert
            Assert.Equal(3, result);
        }

        [Fact]
        public void GetAllEventsInCountry_ShouldReturnCorrectCount()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var townsService = new TownsService(context, this.Mapper, null, null);
            var service = new EventsService(context, this.Mapper, null, null, townsService);

            context.Countries.Add(new Country { Name = "Bulgaria" });
            context.Countries.Add(new Country { Name = "Germany" });

            context.Towns.Add(new Town { Name = "Sofia", CountryId = 1 });
            context.Towns.Add(new Town { Name = "Berlin", CountryId = 2 });

            context.Venues.Add(new Venue { Name = "First Venue", TownId = 1 });
            context.Venues.Add(new Venue { Name = "Second Venue", TownId = 1 });
            context.Venues.Add(new Venue { Name = "Third Venue", TownId = 2 });

            context.Events.Add(new Event { EventName = "First test", VenueId = 1 });
            context.Events.Add(new Event { EventName = "Second test", VenueId = 2 });
            context.Events.Add(new Event { EventName = "Third test", VenueId = 3 });
            context.SaveChanges();

            // Act
            var result = service.GetAllEventsInCountry(new SearchCountryViewModel { CountryId = 1 }).Count();

            // Assert
            Assert.Equal(2, result);
        }

        [Fact]
        public void GetEventById_ShouldReturnCorrectViewModel()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var service = new EventsService(context, this.Mapper, null, null, null);

            context.Events.Add(new Event
            {
                EventName = "Test",
                Date = DateTime.ParseExact("20-12-2018 10:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                OrganizationId = 1,
                DisciplineId = 1,
                VenueId = 1,
                NumberOfParticipants = 10
            });
            context.SaveChanges();

            // Act
            var @event = service.GetEventById(1);

            // Expected EventViewModel
            var expectedViewModel = new EventViewModel
            {
                EventName = "Test",
                Date = "20 December 2018, Thursday",
                Time = "10:00",
                NumberOfParticipants = 10
            };

            // Assert
            Assert.True(@event.Equals(expectedViewModel));
        }
    }
}