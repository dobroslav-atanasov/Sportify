using Microsoft.Extensions.DependencyInjection;
using Sportify.Data;
using Sportify.Data.Models;
using Sportify.Data.ViewModels.Events;
using Sportify.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using Xunit;

namespace Sportify.Tests
{
    using System.Linq;

    public class EventsServiceTests : BaseServiceTests
    {
        [Fact]
        public void CreateEventShould_ReturnCorrectObject()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var service = new EventsService(context, this.Mapper, null, null);

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
            var service = new EventsService(context, this.Mapper, null, null);
            context.Add(new Event());
            context.Add(new Event());
            context.Add(new Event());
            context.SaveChanges();

            // Act
            var result = service.GetAllEvents().Count();

            // Assert
            Assert.Equal(3, result);
        }
    }
}
