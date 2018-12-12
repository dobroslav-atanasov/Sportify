namespace Sportify.Services.Tests
{
    using System.Linq;
    using AutoMapper;
    using Data;
    using Data.Models;
    using Data.ViewModels.Sports;
    using global::AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Services;
    using Xunit;

    public class SportsServiceTests
    {
        [Fact]
        public void GetAllUsersShouldReturnCorrectCountUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<SportifyDbContext>()
                .UseInMemoryDatabase("Sportify_Database_Sports_1")
                .Options;

            var context = new SportifyDbContext(options);
            var mapperConfig = new MapperConfiguration(m => m.AddProfile(new MapperProfile()));
            var mapper = mapperConfig.CreateMapper();

            context.Add(new Sport());
            context.Add(new Sport());
            context.SaveChanges();

            var service = new SportsService(context, mapper, null, null);

            var count = service.GetAllSports().Count();
            Assert.Equal(2, count);
        }

        [Fact]
        public void AddSportShouldReturnCorrectCountUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<SportifyDbContext>()
                .UseInMemoryDatabase("Sportify_Database_Sports_2")
                .Options;

            var context = new SportifyDbContext(options);
            var mapperConfig = new MapperConfiguration(m => m.AddProfile(new MapperProfile()));
            var mapper = mapperConfig.CreateMapper();

            var service = new SportsService(context, mapper, null, null);
            service.Add(new AddSportViewModel());

            var count = context.Sports.Count();

            Assert.Equal(1, count);
        }
    }
}