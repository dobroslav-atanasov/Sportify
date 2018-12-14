namespace Sportify.Tests
{
    using System.Linq;
    using AutoMapper;
    using Data;
    using Data.Models;
    using Data.ViewModels.Towns;
    using global::AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Services;
    using Xunit;

    public class TownsServiceTests : BaseServiceTests
    {
        [Fact]
        public void AddTownShould_ReturnCorrectTown()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var service = new TownsService(context, this.Mapper, null, null);

            // Act
            var town = service.AddTown(new AddTownViewModel
            {
                Name = "Sofia",
                CountryId = 1
            });

            // Expected Town
            var expectedTown = new Town
            {
                Id = 1,
                Name = "Sofia",
                CountryId = 1
            };

            // Assert
            Assert.True(town.Equals(expectedTown));
        }

        [Fact]
        public void GetAllTowns_ShouldReturnCorrectCount()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var service = new TownsService(context, this.Mapper, null, null);
            context.Add(new Town());
            context.Add(new Town());
            context.Add(new Town());
            context.SaveChanges();

            // Act
            var result = service.GetAllTowns().Count();

            // Assert
            Assert.Equal(3, result);
        }

        [Fact]
        public void GetTownById_ShouldReturnCorrectTown()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var service = new TownsService(context, this.Mapper, null, null);
            context.Add(new Town { Name = "Sofia" });
            context.SaveChanges();

            // Act
            var town = service.GetTownById(1);

            // Assert
            Assert.NotNull(town);
        }

        [Fact]
        public void IsDeleteTown_ShouldDeleteTown()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var service = new TownsService(context, this.Mapper, null, null);
            context.Add(new Town { Name = "Sofia" });
            context.SaveChanges();

            // Act
            var result = service.IsDeleteTown(new TownViewModel { Id = 1});

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsDeleteTown_ShouldNotDeleteTown()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var service = new TownsService(context, this.Mapper, null, null);
            context.Add(new Town { Name = "Sofia" });
            context.SaveChanges();

            // Act
            var result = service.IsDeleteTown(new TownViewModel { Id = 2 });

            // Assert
            Assert.False(result);
        }
    }
}