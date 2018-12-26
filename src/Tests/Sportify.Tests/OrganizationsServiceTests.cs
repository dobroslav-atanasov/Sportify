namespace Sportify.Tests
{
    using System.Linq;
    using Data;
    using Data.Models;
    using Data.ViewModels.Organizations;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Services;
    using Xunit;

    public class OrganizationsServiceTests : BaseServiceTests
    {
        [Fact]
        public void CreateOrganization_ShouldReturnCorrectCount()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var userManager = this.ServiceProvider.GetRequiredService<UserManager<User>>();
            userManager.CreateAsync(new User { UserName = "George" }, "1234").GetAwaiter().GetResult();
            var service = new OrganizationsService(context, this.Mapper, userManager, null);

            // Act
            var organization = service.Create(new CreateOrganizationViewModel
            {
                Abbreviation = "AAA",
                Name = "Test",
                Description = "Test Description"
            }, "George");

            // Expected Organization
            var expectedOrganization = new Organization
            {
                Id = 1,
                Abbreviation = "AAA",
                Name = "Test",
                Description = "Test Description",
            };

            // Assert
            Assert.True(organization.Equals(expectedOrganization));
        }

        [Fact]
        public void GetAllOrganizations_ShouldReturnCorrectCount()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var service = new OrganizationsService(context, this.Mapper, null, null);
            context.Organizations.Add(new Organization());
            context.Organizations.Add(new Organization());
            context.Organizations.Add(new Organization());
            context.SaveChanges();

            // Act
            var result = service.GetAllOrganizations().Count();

            // Assert
            Assert.Equal(3, result);
        }

        [Fact]
        public void GetOrganizationByUser_ShouldReturnCorrectOrganizationViewModel()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var userManager = this.ServiceProvider.GetRequiredService<UserManager<User>>();
            userManager.CreateAsync(new User { UserName = "George" }, "1234").GetAwaiter().GetResult();
            var service = new OrganizationsService(context, this.Mapper, userManager, null);

            service.Create(new CreateOrganizationViewModel()
            {
                Abbreviation = "FIS",
                Name = "First Test",
                Description = "Test Description",
            }, "George");

            // Act
            var organization = service.GetOrganizationByUser("George");

            // Expected Sport
            var expectedOrganization = new OrganizationViewModel()
            {
                Id = 1,
                Abbreviation = "FIS",
                Name = "First Test",
                Description = "Test Description"
            };

            // Assert
            Assert.True(organization.Equals(expectedOrganization));
        }

        [Fact]
        public void GetOrganizationByUser_ShouldReturnNull()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var userManager = this.ServiceProvider.GetRequiredService<UserManager<User>>();
            userManager.CreateAsync(new User { UserName = "George" }, "1234").GetAwaiter().GetResult();
            var service = new OrganizationsService(context, this.Mapper, userManager, null);

            service.Create(new CreateOrganizationViewModel()
            {
                Abbreviation = "FIS",
                Name = "First Test",
                Description = "Test Description",
            }, "George");

            // Act
            var organization = service.GetOrganizationByUser("Peter");

            // Assert
            Assert.Null(organization);
        }

        [Fact]
        public void UpdateOrganization_ShouldReturnCorrectOrganizationViewModel()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var userManager = this.ServiceProvider.GetRequiredService<UserManager<User>>();
            userManager.CreateAsync(new User { UserName = "George" }, "1234").GetAwaiter().GetResult();
            var service = new OrganizationsService(context, this.Mapper, userManager, null);

            service.Create(new CreateOrganizationViewModel()
            {
                Abbreviation = "FIS",
                Name = "First Test",
                Description = "Test Description",
            }, "George");

            // Act
            var organization = service.UpdateOrganization(new OrganizationViewModel()
            {
                Id = 1,
                Abbreviation = "FIS New",
                Name = "First Test",
                Description = "Test Description New",
            });

            // Expected Venue
            var expectedOrganization = new OrganizationViewModel()
            {
                Id = 1,
                Abbreviation = "FIS New",
                Name = "First Test",
                Description = "Test Description New",
            };

            // Assert
            Assert.True(organization.Equals(expectedOrganization));
        }

        [Fact]
        public void UpdateOrganization_ShouldReturnNull()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var userManager = this.ServiceProvider.GetRequiredService<UserManager<User>>();
            userManager.CreateAsync(new User { UserName = "George" }, "1234").GetAwaiter().GetResult();
            var service = new OrganizationsService(context, this.Mapper, userManager, null);

            service.Create(new CreateOrganizationViewModel()
            {
                Abbreviation = "FIS",
                Name = "First Test",
                Description = "Test Description",
            }, "George");

            // Act
            var organization = service.UpdateOrganization(new OrganizationViewModel()
            {
                Id = 5,
                Abbreviation = "FIS New",
                Name = "First Test",
                Description = "Test Description New",
            });

            // Assert
            Assert.Null(organization);
        }

        [Fact]
        public void CheckUserHasOrganization_ShouldReturnTrue()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var userManager = this.ServiceProvider.GetRequiredService<UserManager<User>>();
            userManager.CreateAsync(new User { UserName = "George" }, "1234").GetAwaiter().GetResult();
            var service = new OrganizationsService(context, this.Mapper, userManager, null);

            service.Create(new CreateOrganizationViewModel()
            {
                Abbreviation = "FIS",
                Name = "First Test",
                Description = "Test Description",
            }, "George");

            // Act
            var result = service.CheckUserHasOrganization("George");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CheckUserHasOrganization_ShouldReturnFalse()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var userManager = this.ServiceProvider.GetRequiredService<UserManager<User>>();
            userManager.CreateAsync(new User { UserName = "George" }, "1234").GetAwaiter().GetResult();
            var service = new OrganizationsService(context, this.Mapper, userManager, null);

            service.Create(new CreateOrganizationViewModel()
            {
                Abbreviation = "FIS",
                Name = "First Test",
                Description = "Test Description",
            }, "George");

            // Act
            var result = service.CheckUserHasOrganization("Peter");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GetOrganizationById_ShouldReturnCorrectOrganizationViewModel()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var userManager = this.ServiceProvider.GetRequiredService<UserManager<User>>();
            userManager.CreateAsync(new User { UserName = "George" }, "1234").GetAwaiter().GetResult();
            var service = new OrganizationsService(context, this.Mapper, userManager, null);

            service.Create(new CreateOrganizationViewModel()
            {
                Abbreviation = "FIS",
                Name = "First Test",
                Description = "Test Description",
            }, "George");

            // Act
            var organization = service.GetOrganizationById(1);

            // Expected Organization
            var expectedOrganization = new OrganizationViewModel()
            {
                Id = 1,
                Abbreviation = "FIS",
                Name = "First Test",
                Description = "Test Description"
            };

            // Assert
            Assert.True(organization.Equals(expectedOrganization));
        }

        [Fact]
        public void DeleteOrganization_ShouldReturnCorrectCount()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var userManager = this.ServiceProvider.GetRequiredService<UserManager<User>>();
            userManager.CreateAsync(new User { UserName = "George" }, "1234").GetAwaiter().GetResult();
            userManager.CreateAsync(new User { UserName = "Peter" }, "1234").GetAwaiter().GetResult();
            var service = new OrganizationsService(context, this.Mapper, userManager, null);

            service.Create(new CreateOrganizationViewModel()
            {
                Abbreviation = "FIS",
                Name = "First Test",
                Description = "Test Description",
            }, "George");

            service.Create(new CreateOrganizationViewModel()
            {
                Abbreviation = "FIFA",
                Name = "FIFA Test",
                Description = "Test Description",
            }, "Peter");

            // Act
            service.DeleteOrganization(new OrganizationViewModel() { Id = 1 });
            var result = context.Organizations.Count();

            // Assert
            Assert.Equal(1, result);
        }
    }
}