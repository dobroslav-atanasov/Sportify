namespace Sportify.Tests
{
    using System.Linq;
    using AutoMapper;
    using Data;
    using Data.Models;
    using global::AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Services;
    using Xunit;

    public class UsersServiceTests
    {
        [Fact]
        public void GetAllUsersShouldReturnCorrectCountUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<SportifyDbContext>()
                .UseInMemoryDatabase("Sportify_Database_Users_1")
                .Options;

            var context = new SportifyDbContext(options);
            var mapperConfig = new MapperConfiguration(m => m.AddProfile(new MapperProfile()));
            var mapper = mapperConfig.CreateMapper();

            context.Add(new User());
            context.Add(new User());
            context.SaveChanges();

            var service = new UsersService(null, null, null, mapper, context);

            var count = service.GetAllUsers().Count();
            Assert.Equal(2, count);
        }
    }
}