namespace Sportify.Services.Tests
{
    using System.Linq;
    using AutoMapper;
    using Data;
    using Data.Models;
    using Data.ViewModels.Disciplines;
    using global::AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Services;
    using Xunit;

    public class DisciplinesServiceTests
    {
        [Fact]
        public void GetAllUsersShouldReturnCorrectCountUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<SportifyDbContext>()
                .UseInMemoryDatabase("Sportify_Database_Disciplines_1")
                .Options;

            var context = new SportifyDbContext(options);
            var mapperConfig = new MapperConfiguration(m => m.AddProfile(new MapperProfile()));
            var mapper = mapperConfig.CreateMapper();

            context.Add(new Discipline());
            context.Add(new Discipline());
            context.SaveChanges();

            var service = new DisciplinesService(context, mapper, null, null);

            var count = service.GetAllDisciplines().Count();
            Assert.Equal(2, count);
        }

        [Fact]
        public void AddSportShouldReturnCorrectCountUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<SportifyDbContext>()
                .UseInMemoryDatabase("Sportify_Database_Disciplines_2")
                .Options;

            var context = new SportifyDbContext(options);
            var mapperConfig = new MapperConfiguration(m => m.AddProfile(new MapperProfile()));
            var mapper = mapperConfig.CreateMapper();

            var service = new DisciplinesService(context, mapper, null, null);
            service.AddDiscipline(new AddDisciplineViewModel());

            var count = context.Disciplines.Count();

            Assert.Equal(1, count);
        }
    }
}