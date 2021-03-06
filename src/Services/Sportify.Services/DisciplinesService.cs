﻿namespace Sportify.Services
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

        public Discipline AddDiscipline(AddDisciplineViewModel model)
        {
            var discipline = this.Mapper.Map<Discipline>(model);

            this.Context.Disciplines.Add(discipline);
            this.Context.SaveChanges();

            return discipline;
        }

        public IEnumerable<DisciplineViewModel> GetDisciplinesBySportId(int id)
        {
            var disciplines = this.Context
                .Disciplines
                .Where(d => d.SportId == id)
                .OrderBy(d => d.Name)
                .AsQueryable();

            var disciplineViewModels = this.Mapper.Map<IQueryable<Discipline>, IEnumerable<DisciplineViewModel>>(disciplines);

            return disciplineViewModels;
        }

        public DisciplineViewModel GetDisciplineById(int id)
        {
            var discipline = this.Context
                .Disciplines
                .FirstOrDefault(d => d.Id == id);

            var disciplineViewModel = this.Mapper.Map<DisciplineViewModel>(discipline);

            return disciplineViewModel;
        }

        public DisciplineViewModel UpdateDiscipline(DisciplineViewModel model)
        {
            var discipline = this.Context
                .Disciplines
                .FirstOrDefault(s => s.Id == model.Id);

            if (discipline == null)
            {
                return null;
            }

            discipline.Name = model.Name;
            discipline.Description = model.Description;
            discipline.SportId = model.SportId;
            this.Context.SaveChanges();

            var disciplineViewModel = this.Mapper.Map<DisciplineViewModel>(discipline);

            return disciplineViewModel;
        }

        public void DeleteDiscipline(DisciplineViewModel model)
        {
            var discipline = this.Context
                .Disciplines
                .FirstOrDefault(d => d.Id == model.Id);

            if (discipline != null)
            {
                this.Context.Disciplines.Remove(discipline);
                this.Context.SaveChanges();
            }
        }
    }
}