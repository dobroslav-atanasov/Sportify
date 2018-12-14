namespace Sportify.Tests
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Data;
    using Data.Models;
    using global::AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Services;
    using Xunit;

    public class UsersServiceTests : BaseServiceTests
    {
        [Fact]
        public void GetAllUsers_ShouldReturnCorrectCount()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var service = new UsersService(context, this.Mapper, null, null, null);
            context.Add(new User());
            context.Add(new User());
            context.Add(new User());
            context.SaveChanges();

            // Act
            var result = service.GetAllUsers().Count();

            // Assert
            Assert.Equal(3, result);
        }

        [Fact]
        public void IsUsernameExist_ShouldReturnTrue()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var userManager = this.ServiceProvider.GetRequiredService<UserManager<User>>();
            var result = userManager.CreateAsync(new User { UserName = "George" }, "1234").GetAwaiter().GetResult();
            var service = new UsersService(context, this.Mapper, userManager, null, null);

            // Act
            var isUsernameExist = service.IsUsernameExist("George");

            // Assert
            Assert.True(isUsernameExist);
        }

        [Fact]
        public void IsUsernameExistShouldReturnFalseUsingDbContext()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var userManager = this.ServiceProvider.GetRequiredService<UserManager<User>>();
            var result = userManager.CreateAsync(new User { UserName = "George" }, "1234").GetAwaiter().GetResult();
            var service = new UsersService(context, this.Mapper, userManager, null, null);

            // Act
            var isUsernameExist = service.IsUsernameExist("Peter");

            // Assert
            Assert.False(isUsernameExist);
        }
    }
}