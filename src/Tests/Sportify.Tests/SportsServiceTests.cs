namespace Sportify.Tests
{
    using System.Linq;
    using Data;
    using Data.Models;
    using Data.ViewModels.Sports;
    using Microsoft.Extensions.DependencyInjection;
    using Services;
    using Xunit;

    public class SportsServiceTests : BaseServiceTests
    {
        [Fact]
        public void GetAllSports_ShouldReturnCorrectCount()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var service = new SportsService(context, this.Mapper, null, null);
            context.Add(new Sport());
            context.Add(new Sport());
            context.SaveChanges();

            // Act
            var result = service.GetAllSports().Count();

            // Assert
            Assert.Equal(2, result);
        }

        [Fact]
        public void AddSportShould_ShouldReturnCorrectSport()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var service = new SportsService(context, this.Mapper, null, null);

            // Act
            var sport = service.Add(new AddSportViewModel
            {
                Name = "Test",
                Description = "Test Description",
                ImageSportUrl = "www.test.com"
            });

            // Expected Sport
            var expectedSport = new Sport
            {
                Id = 1,
                Name = "Test",
                Description = "Test Description",
                ImageSportUrl = "www.test.com"
            };

            // Assert
            Assert.True(sport.Equals(expectedSport));
        }

        [Fact]
        public void GetSportById_ShouldReturnCorrectSportViewModel()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var service = new SportsService(context, this.Mapper, null, null);

            service.Add(new AddSportViewModel
            {
                Name = "Test",
                Description = "Test Description",
                ImageSportUrl = "www.test.com"
            });

            // Act
            var sport = service.GetSportById(1);

            // Expected Sport
            var expectedSport = new SportViewModel
            {
                Id = 1,
                Name = "Test",
                Description = "Test Description",
                ImageSportUrl = "www.test.com"
            };

            // Assert
            Assert.True(sport.Equals(expectedSport));
        }

        [Fact]
        public void UpdateSport_ShouldReturnCorrectSportViewModel()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var service = new SportsService(context, this.Mapper, null, null);

            service.Add(new AddSportViewModel
            {
                Name = "Test",
                Description = "Test Description",
                ImageSportUrl = "www.test.com"
            });

            // Act
            var sport = service.UpdateSport(new SportViewModel{Id = 1, Name = "New Sport", Description = "New Description"});

            // Expected Sport
            var expectedSport = new SportViewModel {Id = 1, Name = "New Sport", Description = "New Description"};

            // Assert
            Assert.True(sport.Equals(expectedSport));
        }
    }
}