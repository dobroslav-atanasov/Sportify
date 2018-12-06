namespace Sportify.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Data.Models;
    using Data.ViewModels.Towns;
    using global::AutoMapper;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class TownsService : ITownsService
    {
        private readonly SportifyDbContext context;
        private readonly IMapper mapper;

        public TownsService(SportifyDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public void AddTown(AddTownViewModel model)
        {
            var town = this.mapper.Map<Town>(model);

            this.context.Towns.Add(town);
            this.context.SaveChanges();
        }

        public IEnumerable<TownViewModel> GetAllTowns()
        {
            var towns = this.context
                .Towns
                .Include(t => t.Country)
                .AsQueryable();

            var townViewModels = this.mapper.Map<IQueryable<Town>, IEnumerable<TownViewModel>>(towns);

            return townViewModels;
        }

        public TownViewModel GetTownById(int id)
        {
            var town = this.context
                .Towns
                .Include(t => t.Country)
                .FirstOrDefault(t => t.Id == id);

            var townViewModel = this.mapper.Map<TownViewModel>(town);

            return townViewModel;
        }

        public bool IsDeleteTown(TownViewModel model)
        {
            var town = this.context
                .Towns
                .Include(t => t.Country)
                .FirstOrDefault(t => t.Id == model.Id);

            if (town != null)
            {
                this.context.Towns.Remove(town);

                this.context.SaveChanges();

                return true;
            }

            return false;
        }
    }
}