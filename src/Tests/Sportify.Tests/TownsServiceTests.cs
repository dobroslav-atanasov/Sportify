namespace Sportify.Services.Tests
{
    using System.Linq;
    using AutoMapper;
    using Data;
    using Data.Models;
    using global::AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class TownsServiceTests
    {
        [Fact]
        public void GetAllTownsShouldReturnsCorrectCountUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<SportifyDbContext>()
                .UseInMemoryDatabase("Sportify_Database_1")
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
    }
}