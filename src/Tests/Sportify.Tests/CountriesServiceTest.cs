namespace Sportify.Services.Tests
{
    using AutoMapper;
    using Data;
    using Data.Models;
    using global::AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using Xunit;

    public class CountriesServiceTest
    {
        [Fact]
        public void GetCountryByIdShouldReturnsCorrectCountry()
        {
            var options = new DbContextOptionsBuilder<SportifyDbContext>()
                .UseInMemoryDatabase(databaseName: "Sportify_Database_1")
                .Options;

            var context = new SportifyDbContext(options);

            context.Add(new Country
            {
                Id = 2,
                Name = "Bulgaria",
                CountryCode = "BUL"
            });
            context.SaveChanges();

            var service = new CountriesService(context, null);

            var country = service.GetCountryById(2);
            Assert.NotNull(country);
        }

        [Fact]
        public void GetCountryByIdShouldReturnsNull()
        {
            var options = new DbContextOptionsBuilder<SportifyDbContext>()
                .UseInMemoryDatabase(databaseName: "Sportify_Database_1")
                .Options;

            var context = new SportifyDbContext(options);
            
            var service = new CountriesService(context, null);

            var country = service.GetCountryById(3);
            Assert.Null(country);
        }

        [Fact]
        public void GetCountryByNameShouldReturnsCorrectCountry()
        {
            var options = new DbContextOptionsBuilder<SportifyDbContext>()
                .UseInMemoryDatabase(databaseName: "Sportify_Database_1")
                .Options;

            var context = new SportifyDbContext(options);

            context.Add(new Country
            {
                Name = "Bulgaria",
                CountryCode = "BUL"
            });
            context.SaveChanges();

            var service = new CountriesService(context, null);

            var country = service.GetCountryByName("Bulgaria");
            Assert.NotNull(country);
        }

        [Fact]
        public void GetCountryByNameShouldReturnsNull()
        {
            var options = new DbContextOptionsBuilder<SportifyDbContext>()
                .UseInMemoryDatabase(databaseName: "Sportify_Database_1")
                .Options;

            var context = new SportifyDbContext(options);

            var service = new CountriesService(context, null);

            var country = service.GetCountryByName("Serbia");
            Assert.Null(country);
        }

        [Fact]
        public void GetAllCountryNamesShouldReturnsCorrectCountUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<SportifyDbContext>()
                .UseInMemoryDatabase(databaseName: "Sportify_Database_2")
                .Options;

            var context = new SportifyDbContext(options);
            var mapperConfig = new MapperConfiguration(m => m.AddProfile(new MapperProfile()));
            var mapper = mapperConfig.CreateMapper();

            context.Add(new Country());
            context.Add(new Country());
            context.SaveChanges();

            var service = new CountriesService(context, mapper);

            var count = service.GetAllCountryNames().Count();
            Assert.Equal(2, count);
        }
    }
}
