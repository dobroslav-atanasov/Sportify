namespace Sportify.Services.Tests
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

            var service = new UsersService(context, mapper, null, null, null);

            var count = service.GetAllUsers().Count();
            Assert.Equal(2, count);
        }

        [Fact]
        public void IsUsernameExistShouldReturnTrueUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<SportifyDbContext>()
                .UseInMemoryDatabase("Sportify_Database_Users_2")
                .Options;

            var context = new SportifyDbContext(options);

            context.Add(new User { UserName = "Peter"});
            context.SaveChanges();

            var service = new UsersService(context, null, null, null, null);

            var result = service.IsUsernameExist("Peter");
            Assert.True(result);
        }

        [Fact]
        public void IsUsernameExistShouldReturnFalseUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<SportifyDbContext>()
                .UseInMemoryDatabase("Sportify_Database_Users_3")
                .Options;

            var context = new SportifyDbContext(options);

            context.Add(new User { UserName = "Peter" });
            context.SaveChanges();

            var service = new UsersService(context, null, null, null, null);

            var result = service.IsUsernameExist("George");
            Assert.False(result);
        }
    }
}