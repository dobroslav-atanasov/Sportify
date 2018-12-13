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
    using Sportify.Data.ViewModels.Users;
    using Sportify.Services.Interfaces;
    using Xunit;

    public class UsersServiceTests : BaseServiceTests
    {
        [Fact]
        public void CreateAccount_ShouldReturnCorrectResult()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var userManager = this.ServiceProvider.GetRequiredService<UserManager<User>>();
            var signInManager = this.ServiceProvider.GetRequiredService<SignInManager<User>>();
            var countriesService = new CountriesService(context, this.Mapper, userManager, signInManager);
            var service = new UsersService(context, this.Mapper, userManager, signInManager, countriesService);

            context.Roles.Add(new IdentityRole{ Name= "Administrator", NormalizedName = "ADMINISTRATOR"});
            context.Roles.Add(new IdentityRole{ Name = "Editor", NormalizedName = "EDITOR" });
            context.Roles.Add(new IdentityRole{ Name = "User", NormalizedName = "USER" });
            
            context.Countries.Add(new Country { Name = "Bulgaria" });
            context.SaveChanges();

            // Act
            var result = service.CreateAccountAsync(new CreateAccountViewModel
            {
                Username = "George",
                Email = "george@test.com",
                Password = "1234",
                ConfirmPassword = "1234",
                BirthDate = DateTime.ParseExact("05.05.2000", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                CountryId = 1
            }).GetAwaiter().GetResult();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void SignIn_ShouldReturnCorrectResult()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var userManager = this.ServiceProvider.GetRequiredService<UserManager<User>>();
            var signInManager = this.ServiceProvider.GetRequiredService<SignInManager<User>>();
            var service = new UsersService(context, this.Mapper, userManager, signInManager, null);

            userManager.CreateAsync(new User { UserName = "George" }, "1234").GetAwaiter().GetResult();

            // Act
            var result = service.SignIn(new SignInViewModel { Username = "George", Password = "1234" });

            // Assert
            Assert.True(result);
        }

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

            context.Add(new User { UserName = "Peter" });
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