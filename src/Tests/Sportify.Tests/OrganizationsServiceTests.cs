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
            service.Create(new CreateOrganizationViewModel(), "George");
            var result = service.Context.Organizations.Count();

            // Assert
            Assert.Equal(1, result);
        }
    }
}
