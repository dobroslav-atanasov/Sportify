namespace Sportify.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Data.Models;
    using Data.ViewModels.Towns;
    using global::AutoMapper;
    using Interfaces;
    using Microsoft.AspNetCore.Identity;

    public class TownsService : BaseService, ITownsService
    {
        public TownsService(SportifyDbContext context, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
            : base(context, mapper, userManager, signInManager)
        {
        }

        public Town AddTown(AddTownViewModel model)
        {
            var town = this.Mapper.Map<Town>(model);

            this.Context.Towns.Add(town);
            this.Context.SaveChanges();

            return town;
        }

        public IEnumerable<TownViewModel> GetAllTowns()
        {
            var towns = this.Context
                .Towns
                .OrderBy(t => t.Name)
                .AsQueryable();

            var townViewModels = this.Mapper.Map<IQueryable<Town>, IEnumerable<TownViewModel>>(towns);

            return townViewModels;
        }

        public TownViewModel GetTownById(int id)
        {
            var town = this.Context
                .Towns
                .FirstOrDefault(t => t.Id == id);

            var townViewModel = this.Mapper.Map<TownViewModel>(town);

            return townViewModel;
        }

        public bool IsDeleteTown(TownViewModel model)
        {
            var town = this.Context
                .Towns
                .FirstOrDefault(t => t.Id == model.Id);

            if (town != null)
            {
                this.Context.Towns.Remove(town);

                this.Context.SaveChanges();

                return true;
            }

            return false;
        }

        public IList<int> GetAllTownIdsByCountryId(int id)
        {
            var towns = this.Context
                .Towns
                .Where(t => t.CountryId == id)
                .Select(t => t.Id)
                .ToList();

            return towns;
        }
    }
}