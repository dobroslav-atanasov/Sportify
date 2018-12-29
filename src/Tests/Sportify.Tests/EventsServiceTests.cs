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
            context.Venues.Add(new Venue { Name = "Third Venue", TownId = 1 });
            context.Venues.Add(new Venue { Name = "Fourth Venue", TownId = 2 });

            context.Events.Add(new Event
            {
                EventName = "First test",
                VenueId = 1,
                Date = DateTime.ParseExact("25-01-2019 08:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture)
            });

            context.Events.Add(new Event
            {
                EventName = "Second test",
                VenueId = 2,
                Date = DateTime.ParseExact("20-01-2019 10:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture)
            });

            context.Events.Add(new Event
            {
                EventName = "Third test",
                VenueId = 3,
                Date = DateTime.ParseExact("20-12-2018 10:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture)
            });

            context.Events.Add(new Event
            {
                EventName = "Fourth test",
                VenueId = 4,
                Date = DateTime.ParseExact("28-12-2018 10:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture)
            });
            context.SaveChanges();

            // Act
            var result = service.GetAllEventsInCountry(new SearchCountryViewModel { CountryId = 1 }).Count();

            // Assert
            Assert.Equal(3, result);
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

        [Fact]
        public void IsUserParticipate_ShouldReturnTrue()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var service = new EventsService(context, this.Mapper, null, null, null);

            context.Users.Add(new User { Id = "test1", UserName = "George" });
            context.SaveChanges();

            context.Participants.Add(new Participant
            {
                UserId = "test1",
                EventId = 1
            });
            context.SaveChanges();

            // Act
            var result = service.IsUserParticipate("test1", 1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsUserParticipate_ShouldReturnFalse()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var service = new EventsService(context, this.Mapper, null, null, null);

            context.Users.Add(new User { Id = "test1", UserName = "George" });
            context.SaveChanges();

            context.Participants.Add(new Participant
            {
                UserId = "test1",
                EventId = 1
            });
            context.SaveChanges();

            // Act
            var result = service.IsUserParticipate("test2", 2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void JoinUserToEvent_ShouldReturnCorrectParticipant()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var service = new EventsService(context, this.Mapper, null, null, null);

            context.Users.Add(new User { Id = "test1", UserName = "George" });
            context.Events.Add(new Event { EventName = "Test Event" });
            context.SaveChanges();

            // Act
            var participant = service.JoinUserToEvent("test1", 1);

            // Expected Participant
            var expectedParticipant = new Participant
            {
                UserId = "test1",
                EventId = 1
            };

            // Assert
            Assert.True(participant.Equals(expectedParticipant));
        }

        [Fact]
        public void LeaveUserFromEvent_ShouldReturnCorrectParticipant()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var service = new EventsService(context, this.Mapper, null, null, null);

            context.Users.Add(new User { Id = "test1", UserName = "George" });
            context.Events.Add(new Event { EventName = "Test Event" });
            context.SaveChanges();
            service.JoinUserToEvent("test1", 1);

            // Act
            service.LeaveUserFromEvent("test1", 1);
            var result = context.Participants.Count();

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void GetEventsByUser_ShouldReturnCorrectCount()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var service = new EventsService(context, this.Mapper, null, null, null);
            context.Users.Add(new User
            {
                Id = "test1",
                UserName = "George"
            });

            context.Organizations.Add(new Organization
            {
                Name = "FIS",
                PresidentId = "test1"
            });
            context.SaveChanges();

            service.Create(new CreateEventViewModel
            {
                EventName = "First Event",
                Date = DateTime.ParseExact("25-05-2019 10:00", "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture),
                OrganizationId = 1,
                VenueId = 1,
                DisciplineId = 1,
                NumberOfParticipants = 10
            });

            service.Create(new CreateEventViewModel
            {
                EventName = "Second Event",
                Date = DateTime.ParseExact("05-10-2019 11:00", "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture),
                OrganizationId = 2,
                VenueId = 2,
                DisciplineId = 2,
                NumberOfParticipants = 20
            });

            service.Create(new CreateEventViewModel
            {
                EventName = "Third Event",
                Date = DateTime.ParseExact("10-01-2019 09:00", "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture),
                OrganizationId = 1,
                VenueId = 2,
                DisciplineId = 2,
                NumberOfParticipants = 50
            });

            // Act
            var result = service.GetEventsByUser("George").Count();

            // Assert
            Assert.Equal(2, result);
        }

        [Fact]
        public void GetEventForUpdateById_ShouldReturnCorrectUpdateEventViewModel()
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
            var @event = service.GetEventForUpdateById(1);

            // Expected UpdateEventViewModel
            var expectedViewModel = new UpdateEventViewModel()
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
            Assert.True(@event.Equals(expectedViewModel));
        }

        [Fact]
        public void UpdateEvent_ShouldReturnCorrectUpdateEventViewModel()
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
            var @event = service.UpdateEvent(new UpdateEventViewModel()
            {
                Id = 1,
                EventName = "New Name",
                Date = DateTime.ParseExact("30-12-2018 11:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                OrganizationId = 12,
                DisciplineId = 2,
                VenueId = 12,
                NumberOfParticipants = 20
            });

            // Expected UpdateEventViewModel
            var expectedEvent = new UpdateEventViewModel()
            {
                Id = 1,
                EventName = "New Name",
                Date = DateTime.ParseExact("30-12-2018 11:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                OrganizationId = 12,
                DisciplineId = 2,
                VenueId = 12,
                NumberOfParticipants = 20
            };

            // Assert
            Assert.True(@event.Equals(expectedEvent));
        }

        [Fact]
        public void UpdateEvent_ShouldReturnNull()
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
            var @event = service.UpdateEvent(new UpdateEventViewModel()
            {
                EventName = "Test",
                Date = DateTime.ParseExact("20-12-2018 10:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                OrganizationId = 1,
                DisciplineId = 1,
                VenueId = 1,
                NumberOfParticipants = 10
            });

            // Assert
            Assert.Null(@event);
        }

        [Fact]
        public void DeleteEvent_ShouldReturnCorrectCount()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var service = new EventsService(context, this.Mapper, null, null, null);

            service.Create(new CreateEventViewModel
            {
                EventName = "First Event",
                Date = DateTime.ParseExact("20-12-2018 10:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                OrganizationId = 1,
                DisciplineId = 1,
                VenueId = 1,
                NumberOfParticipants = 10
            });

            service.Create(new CreateEventViewModel
            {
                EventName = "Second Event",
                Date = DateTime.ParseExact("20-05-2018 12:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                OrganizationId = 2,
                DisciplineId = 2,
                VenueId = 3,
                NumberOfParticipants = 20
            });

            service.Create(new CreateEventViewModel
            {
                EventName = "Third Event",
                Date = DateTime.ParseExact("02-12-2019 12:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                OrganizationId = 2,
                DisciplineId = 2,
                VenueId = 2,
                NumberOfParticipants = 10
            });

            // Act
            service.DeleteEvent(new EventViewModel { Id = 1 });
            var result = context.Events.Count();

            // Assert
            Assert.Equal(2, result);
        }

        [Fact]
        public void CheckForFreeSpace_ShouldReturnCorrectTrue()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var service = new EventsService(context, this.Mapper, null, null, null);

            service.Create(new CreateEventViewModel
            {
                EventName = "First Event",
                Date = DateTime.ParseExact("20-12-2018 10:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                OrganizationId = 1,
                DisciplineId = 1,
                VenueId = 1,
                NumberOfParticipants = 3
            });

            context.Users.Add(new User { Id = "test1", UserName = "George" });
            context.Users.Add(new User { Id = "test2", UserName = "Peter" });

            context.Participants.Add(new Participant
            {
                EventId = 1,
                UserId = "test1"
            });

            context.Participants.Add(new Participant
            {
                EventId = 1,
                UserId = "test2"
            });

            context.SaveChanges();

            // Act
            var result = service.CheckForFreeSpace(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CheckForFreeSpace_ShouldReturnCorrectFalse()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var service = new EventsService(context, this.Mapper, null, null, null);

            service.Create(new CreateEventViewModel
            {
                EventName = "First Event",
                Date = DateTime.ParseExact("20-12-2018 10:00", "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                OrganizationId = 1,
                DisciplineId = 1,
                VenueId = 1,
                NumberOfParticipants = 2
            });

            context.Users.Add(new User { Id = "test1", UserName = "George" });
            context.Users.Add(new User { Id = "test2", UserName = "Peter" });

            context.Participants.Add(new Participant
            {
                EventId = 1,
                UserId = "test1"
            });

            context.Participants.Add(new Participant
            {
                EventId = 1,
                UserId = "test2"
            });

            context.SaveChanges();

            // Act
            var result = service.CheckForFreeSpace(1);

            // Assert
            Assert.False(result);
        }
    }
}