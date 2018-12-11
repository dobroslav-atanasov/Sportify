namespace Sportify.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Data.Models;
    using Data.ViewModels.Disciplines;
    using global::AutoMapper;
    using Interfaces;
    using Microsoft.AspNetCore.Identity;

    public class DisciplinesService : BaseService, IDisciplinesService
    {
        public DisciplinesService(SportifyDbContext context, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
            : base(context, mapper, userManager, signInManager)
        {
        }

        public IEnumerable<DisciplineViewModel> GetAllDisciplines()
        {
            var disciplines = this.Context
                .Disciplines
                .OrderBy(s => s.Name)
                .AsQueryable();

            var disciplinesViewModel = this.Mapper.Map<IQueryable<Discipline>, IEnumerable<DisciplineViewModel>>(disciplines);

            return disciplinesViewModel;
        }

        public void AddDiscipline(AddDisciplineViewModel model)
        {
            var discipline = this.Mapper.Map<Discipline>(model);

            this.Context.Disciplines.Add(discipline);
            this.Context.SaveChanges();
        }
    }
}