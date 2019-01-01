namespace Sportify.Tests
{
    using System.Linq;

    using Data;
    using Data.Models;
    using Microsoft.Extensions.DependencyInjection;
    using Services;
    using Xunit;

    public class CountriesServiceTest : BaseServiceTests
    {
        [Fact]
        public void GetCountryById_ShouldReturnCorrectCountry()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var service = new CountriesService(context, this.Mapper, null, null);
            context.Add(new Country());
            context.SaveChanges();

            // Act
            var country = service.GetCountryById(1);

            // Assert
            Assert.NotNull(country);
        }

        [Fact]
        public void GetCountryById_ShouldReturnNull()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var service = new CountriesService(context, this.Mapper, null, null);

            // Act
            var country = service.GetCountryById(1);

            // Assert
            Assert.Null(country);
        }

        [Fact]
        public void GetCountryByName_ShouldReturnCorrectCountry()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var service = new CountriesService(context, this.Mapper, null, null);
            context.Add(new Country { Name = "Bulgaria" });
            context.SaveChanges();

            // Act
            var country = service.GetCountryByName("Bulgaria");

            // Assert
            Assert.NotNull(country);
        }

        [Fact]
        public void GetCountryByName_ShouldReturnNull()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var service = new CountriesService(context, this.Mapper, null, null);
            context.Add(new Country { Name = "Bulgaria" });
            context.SaveChanges();

            // Act
            var country = service.GetCountryByName("Serbia");

            // Assert
            Assert.Null(country);
        }

        [Fact]
        public void GetAllCountryNames_ShouldReturnCorrectCount()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var service = new CountriesService(context, this.Mapper, null, null);
            context.Add(new Country());
            context.Add(new Country());
            context.Add(new Country());
            context.SaveChanges();

            // Act
            var result = service.GetAllCountryNames().Count();

            // Assert
            Assert.Equal(3, result);
        }
    }
}