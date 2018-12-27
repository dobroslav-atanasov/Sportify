namespace Sportify.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    using Data;
    using Data.Models;
    using Data.ViewModels.Events;
    using Data.ViewModels.Participants;
    using Microsoft.Extensions.DependencyInjection;
    using Services;
    using Xunit;

    public class ParticipantsServiceTests : BaseServiceTests
    {

        [Fact]
        public void GetParticipantsInEventId_ShouldReturnCorrectCount()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var service = new ParticipantsService(context, this.Mapper, null, null);
            var eventService = new EventsService(context, this.Mapper, null, null, null);

            context.Users.Add(new User
            {
                Id = "test1",
                UserName = "Peter"
            });

            context.Users.Add(new User
            {
                Id = "test2",
                UserName = "George"
            });
            context.SaveChanges();

            eventService.Create(new CreateEventViewModel
            {
                EventName = "First Event",
                Date = DateTime.ParseExact("20-12-2018 10:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                OrganizationId = 1,
                DisciplineId = 1,
                VenueId = 1,
                NumberOfParticipants = 10
            });

            context.Participants.Add(new Participant
            {
                UserId = "test1",
                EventId = 1
            });

            context.Participants.Add(new Participant
            {
                UserId = "test2",
                EventId = 1
            });

            context.SaveChanges();

            // Act
            var result = service.GetParticipantsInEventId(1).Count;

            // Assert
            Assert.Equal(2, result);
        }

        [Fact]
        public void SetUpResults_ShouldReturnCorrectResult()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var service = new ParticipantsService(context, this.Mapper, null, null);
            var eventService = new EventsService(context, this.Mapper, null, null, null);

            context.Users.Add(new User
            {
                Id = "test1",
                UserName = "Peter"
            });

            context.Users.Add(new User
            {
                Id = "test2",
                UserName = "George"
            });
            context.SaveChanges();

            eventService.Create(new CreateEventViewModel
            {
                EventName = "First Event",
                Date = DateTime.ParseExact("20-12-2018 10:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                OrganizationId = 1,
                DisciplineId = 1,
                VenueId = 1,
                NumberOfParticipants = 10
            });

            context.Participants.Add(new Participant
            {
                UserId = "test1",
                EventId = 1
            });

            context.Participants.Add(new Participant
            {
                UserId = "test2",
                EventId = 1
            });

            context.SaveChanges();

            // Act
            var result = service.SetUpResults(1, new List<ParticipantViewModel>
            {
                new ParticipantViewModel
                {
                    UserId = "test1",
                    EventId = 1,
                    Result = DateTime.ParseExact("20-12-2018 01:02:03", "dd-MM-yyyy hh:mm:ss", CultureInfo.InvariantCulture)
                },
                new ParticipantViewModel
                {
                    UserId = "test2",
                    EventId = 1,
                    Result = DateTime.ParseExact("20-12-2018 02:03:04", "dd-MM-yyyy hh:mm:ss", CultureInfo.InvariantCulture)
                },
            });

            // Expected List
            var expectedList = new List<ParticipantViewModel>
            {
                new ParticipantViewModel
                {
                    UserId = "test1",
                    EventId = 1,
                    Result = DateTime.ParseExact("20-12-2018 01:02:03", "dd-MM-yyyy hh:mm:ss", CultureInfo.InvariantCulture)
                },
                new ParticipantViewModel
                {
                    UserId = "test2",
                    EventId = 1,
                    Result = DateTime.ParseExact("20-12-2018 02:03:04", "dd-MM-yyyy hh:mm:ss", CultureInfo.InvariantCulture)
                },
            };

            // Assert
            Assert.Equal(expectedList, result);
        }
    }
}