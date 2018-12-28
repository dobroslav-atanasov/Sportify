namespace Sportify.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using Data;
    using Data.Models;
    using Data.ViewModels.Events;
    using Data.ViewModels.Participants;
    using Microsoft.AspNetCore.Identity;
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
        public void SetResults_ShouldReturnCorrectCount()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var userManager = this.ServiceProvider.GetRequiredService<UserManager<User>>();
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
            var result = service.SetResults(1, new List<ParticipantViewModel>
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
            }).Count;

            //// Expected List
            //var firstUser = userManager.FindByNameAsync("Peter").GetAwaiter().GetResult();
            //var secondUser = userManager.FindByNameAsync("George").GetAwaiter().GetResult();

            //var expectedList = new List<ParticipantViewModel>
            //{
            //    new ParticipantViewModel
            //    {
            //        UserId = "test1",
            //        EventId = 1,
            //        Result = DateTime.ParseExact("20-12-2018 01:02:03", "dd-MM-yyyy hh:mm:ss", CultureInfo.InvariantCulture),
            //        User = firstUser,
            //        Event = new Event
            //        {
            //            Id = 1,
            //            EventName = "First Event",
            //            Date = DateTime.ParseExact("20-12-2018 10:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
            //            OrganizationId = 1,
            //            DisciplineId = 1,
            //            VenueId = 1,
            //            NumberOfParticipants = 10
            //        }
            //    },
            //    new ParticipantViewModel
            //    {
            //        UserId = "test2",
            //        EventId = 1,
            //        Result = DateTime.ParseExact("20-12-2018 02:03:04", "dd-MM-yyyy hh:mm:ss", CultureInfo.InvariantCulture),
            //        User = secondUser,
            //        Event = new Event
            //        {
            //            Id = 1,
            //            EventName = "First Event",
            //            Date = DateTime.ParseExact("20-12-2018 10:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
            //            OrganizationId = 1,
            //            DisciplineId = 1,
            //            VenueId = 1,
            //            NumberOfParticipants = 10
            //        }
            //    },
            //};

            // Assert
            Assert.Equal(2, result);
        }

        [Fact]
        public void GetResultByUser_ShouldReturnCorrectCount()
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

            eventService.Create(new CreateEventViewModel
            {
                EventName = "Second Event",
                Date = DateTime.ParseExact("30-12-2018 10:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                OrganizationId = 1,
                DisciplineId = 1,
                VenueId = 1,
                NumberOfParticipants = 10
            });

            context.Participants.Add(new Participant
            {
                UserId = "test1",
                EventId = 1,
                Result = DateTime.ParseExact("25-12-2018 01:02:25.123", "dd-MM-yyyy hh:mm:ss.fff", CultureInfo.InvariantCulture),
            });

            context.Participants.Add(new Participant
            {
                UserId = "test1",
                EventId = 2
            });

            context.Participants.Add(new Participant
            {
                UserId = "test2",
                EventId = 1
            });

            context.SaveChanges();

            // Act
            var result = service.GetResultByUser("Peter").Count();

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void GetEventsWithMyParticipation_ShouldReturnCorrectCount()
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

            eventService.Create(new CreateEventViewModel
            {
                EventName = "Second Event",
                Date = DateTime.ParseExact("10-01-2019 10:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                OrganizationId = 1,
                DisciplineId = 1,
                VenueId = 1,
                NumberOfParticipants = 10
            });

            eventService.Create(new CreateEventViewModel
            {
                EventName = "Second Event",
                Date = DateTime.ParseExact("20-01-2019 12:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
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
                UserId = "test1",
                EventId = 2
            });

            context.Participants.Add(new Participant
            {
                UserId = "test1",
                EventId = 3
            });

            context.SaveChanges();

            // Act
            var result = service.GetEventsWithMyParticipation("Peter").Count();

            // Assert
            Assert.Equal(2, result);
        }
    }
}