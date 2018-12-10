namespace Sportify.Services.Tests
{
    using System.Linq;
    using AutoMapper;
    using Data;
    using Data.Models;
    using Data.ViewModels.Towns;
    using global::AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class TownsServiceTests
    {
        [Fact]
        public void AddTownShouldReturnsorrectCountUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<SportifyDbContext>()
                .UseInMemoryDatabase("Sportify_Database_Towns")
                .Options;

            var context = new SportifyDbContext(options);
            var mapperConfig = new MapperConfiguration(m => m.AddProfile(new MapperProfile()));
            var mapper = mapperConfig.CreateMapper();

            var service = new TownsService(context, mapper);
            service.AddTown(new AddTownViewModel());

            var count = context.Towns.Count();

            Assert.Equal(1, count);
        }

        [Fact]
        public void GetAllTownsShouldReturnCorrectCountUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<SportifyDbContext>()
                .UseInMemoryDatabase("Sportify_Database_Towns_3")
                .Options;

            var context = new SportifyDbContext(options);
            var mapperConfig = new MapperConfiguration(m => m.AddProfile(new MapperProfile()));
            var mapper = mapperConfig.CreateMapper();

            context.Add(new Town());
            context.Add(new Town());
            context.SaveChanges();

            var service = new TownsService(context, mapper);

            var count = service.GetAllTowns().Count();
            Assert.Equal(2, count);
        }

        [Fact]
        public void GetTownByIdShouldReturnCorrectTowns()
        {
            var options = new DbContextOptionsBuilder<SportifyDbContext>()
                .UseInMemoryDatabase(databaseName: "Sportify_Database_Towns_4")
                .Options;

            var context = new SportifyDbContext(options);
            var mapperConfig = new MapperConfiguration(m => m.AddProfile(new MapperProfile()));
            var mapper = mapperConfig.CreateMapper();

            context.Add(new Town
            {
                Id = 1,
                Name = "Sofia"
            });
            context.SaveChanges();

            var service = new TownsService(context, mapper);

            var country = service.GetTownById(1);
            Assert.NotNull(country);
        }

        [Fact]
        public void IsDeleteTownShouldDeleteTown()
        {
            var options = new DbContextOptionsBuilder<SportifyDbContext>()
                .UseInMemoryDatabase(databaseName: "Sportify_Database_Towns_5")
                .Options;

            var context = new SportifyDbContext(options);
            var mapperConfig = new MapperConfiguration(m => m.AddProfile(new MapperProfile()));
            var mapper = mapperConfig.CreateMapper();

            context.Add(new Town
            {
                Id = 1,
                Name = "Sofia"
            });
            context.SaveChanges();

            var service = new TownsService(context, mapper);
            var isDeleteTown = service.IsDeleteTown(new TownViewModel { Id = 1 });

            Assert.True(isDeleteTown);
        }

        [Fact]
        public void IsDeleteTownShouldNotDeleteTown()
        {
            var options = new DbContextOptionsBuilder<SportifyDbContext>()
                .UseInMemoryDatabase(databaseName: "Sportify_Database_Towns_6")
                .Options;

            var context = new SportifyDbContext(options);
            var mapperConfig = new MapperConfiguration(m => m.AddProfile(new MapperProfile()));
            var mapper = mapperConfig.CreateMapper();

            context.Add(new Town
            {
                Id = 2,
                Name = "Sofia"
            });
            context.SaveChanges();

            var service = new TownsService(context, mapper);
            var isDeleteTown = service.IsDeleteTown(new TownViewModel { Id = 1 });

            Assert.False(isDeleteTown);
        }
    }
}