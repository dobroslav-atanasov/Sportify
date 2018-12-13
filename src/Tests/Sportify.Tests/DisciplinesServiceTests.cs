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
        public void AddDisciplineShould_ReturnCorrectCount()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var service = new DisciplinesService(context, this.Mapper, null, null);

            // Act
            service.AddDiscipline(new AddDisciplineViewModel());
            var result = context.Disciplines.Count();

            // Assert
            Assert.Equal(1, result);
        }
    }
}