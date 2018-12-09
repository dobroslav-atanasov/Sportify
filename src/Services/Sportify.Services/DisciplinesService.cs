namespace Sportify.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Data.Models;
    using Data.ViewModels.Disciplines;
    using global::AutoMapper;
    using Interfaces;

    public class DisciplinesService : IDisciplinesService
    {
        private readonly SportifyDbContext context;
        private readonly IMapper mapper;

        public DisciplinesService(SportifyDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IEnumerable<DisciplineViewModel> GetAllDisciplines()
        {
            var disciplines = this.context
                .Disciplines
                .OrderBy(s => s.Name)
                .AsQueryable();

            var disciplinesViewModel = this.mapper.Map<IQueryable<Discipline>, IEnumerable<DisciplineViewModel>>(disciplines);

            return disciplinesViewModel;
        }

        public void AddDiscipline(AddDisciplineViewModel model)
        {
            var discipline = this.mapper.Map<Discipline>(model);

            this.context.Disciplines.Add(discipline);
            this.context.SaveChanges();
        }
    }
}
