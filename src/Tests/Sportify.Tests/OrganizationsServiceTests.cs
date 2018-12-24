using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Sportify.Data;
using Sportify.Data.Models;
using Sportify.Data.ViewModels.Organizations;
using Sportify.Services;
using System.Linq;
using Xunit;

namespace Sportify.Tests
{
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
    }
}
