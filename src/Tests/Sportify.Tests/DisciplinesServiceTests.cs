namespace Sportify.Tests
{
    using System.Linq;
    using AutoMapper;
    using Data;
    using Data.Models;
    using Data.ViewModels.Disciplines;
    using global::AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Services;
    using Xunit;

    public class DisciplinesServiceTests : BaseServiceTests
    {
        [Fact]
        public void GetGetAllDisciplines_ShouldReturnCorrectCount()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var service = new DisciplinesService(context, this.Mapper, null, null);
            context.Add(new Discipline());
            context.Add(new Discipline());
            context.Add(new Discipline());
            context.SaveChanges();

            // Act
            var result = service.GetAllDisciplines().Count();

            // Assert
            Assert.Equal(3, result);
        }

        [Fact]
        public void AddDisciplineShould_ReturnCorrectDiscipline()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var service = new DisciplinesService(context, this.Mapper, null, null);

            // Act
            var discipline = service.AddDiscipline(new AddDisciplineViewModel
            {
                Name = "Test",
                Description = "Test Description",
                SportId = 1
            });

            // Expected Discipline
            var expectedDiscipline = new Discipline
            {
                Id = 1,
                Name = "Test",
                Description = "Test Description",
                SportId = 1
            };

            // Assert
            Assert.True(discipline.Equals(expectedDiscipline));
        }
    }
}