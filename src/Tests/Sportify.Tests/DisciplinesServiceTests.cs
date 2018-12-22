namespace Sportify.Tests
{
    using System.Linq;
    using Data;
    using Data.Models;
    using Data.ViewModels.Disciplines;
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
        public void AddDisciplineShould_ShouldReturnCorrectDiscipline()
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

        [Fact]
        public void GetDisciplinesBySportId_ShouldReturnCorrectDiscipline()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var service = new DisciplinesService(context, this.Mapper, null, null);

            service.AddDiscipline(new AddDisciplineViewModel
            {
                Name = "Test 1",
                Description = "Test Description 1",
                SportId = 1
            });

            service.AddDiscipline(new AddDisciplineViewModel
            {
                Name = "Test 2",
                Description = "Test Description 2",
                SportId = 1
            });

            service.AddDiscipline(new AddDisciplineViewModel
            {
                Name = "Test 3",
                Description = "Test Description 3",
                SportId = 2
            });

            // Act
            var result = service.GetDisciplinesBySportId(1).Count();

            // Assert
            Assert.Equal(2, result);
        }

        [Fact]
        public void GetDisciplineById_ShouldReturnCorrectDisciplineViewModel()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var service = new DisciplinesService(context, this.Mapper, null, null);

            service.AddDiscipline(new AddDisciplineViewModel()
            {
                Name = "Test",
                Description = "Test Description",
                SportId = 1
            });

            // Act
            var discipline = service.GetDisciplineById(1);

            // Expected Sport
            var expectedDiscipline = new DisciplineViewModel()
            {
                Id = 1,
                Name = "Test",
                Description = "Test Description",
                SportId = 1
            };

            // Assert
            Assert.True(discipline.Equals(expectedDiscipline));
        }

        [Fact]
        public void UpdateDiscipline_ShouldReturnCorrectDisciplineViewModel()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var service = new DisciplinesService(context, this.Mapper, null, null);

            service.AddDiscipline(new AddDisciplineViewModel()
            {
                Name = "Test",
                Description = "Test Description",
                SportId = 1
            });

            // Act
            var discipline = service.UpdateDiscipline(new DisciplineViewModel
            {
                Id = 1,
                Name = "New Discipline",
                Description = "New Description",
                SportId = 5
            });

            // Expected Sport
            var expectedDiscipline = new DisciplineViewModel
            {
                Id = 1,
                Name = "New Discipline",
                Description = "New Description",
                SportId = 5
            };

            // Assert
            Assert.True(discipline.Equals(expectedDiscipline));
        }

        [Fact]
        public void UpdateDiscipline_ShouldReturnNull()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var service = new DisciplinesService(context, this.Mapper, null, null);

            service.AddDiscipline(new AddDisciplineViewModel()
            {
                Name = "Test",
                Description = "Test Description",
                SportId = 1
            });

            // Act
            var discipline = service.UpdateDiscipline(new DisciplineViewModel
            {
                Id = 5,
                Name = "New Discipline",
                Description = "New Description",
                SportId = 5
            });

            // Assert
            Assert.Null(discipline);
        }

        [Fact]
        public void DeleteDiscipline_ShouldReturnCorrectCount()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<SportifyDbContext>();
            var service = new DisciplinesService(context, this.Mapper, null, null);

            service.AddDiscipline(new AddDisciplineViewModel()
            {
                Name = "Test",
                Description = "Test Description",
                SportId = 1
            });

            service.AddDiscipline(new AddDisciplineViewModel()
            {
                Name = "Test Discipline",
                Description = "Test Description",
                SportId = 2
            });

            // Act
            service.DeleteDiscipline(new DisciplineViewModel { Id = 1 });
            var result = context.Disciplines.Count();

            // Assert
            Assert.Equal(1, result);
        }
    }
}