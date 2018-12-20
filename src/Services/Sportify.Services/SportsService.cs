namespace Sportify.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Data.Models;
    using Data.ViewModels.Sports;
    using global::AutoMapper;
    using Interfaces;
    using Microsoft.AspNetCore.Identity;

    public class SportsService : BaseService, ISportsService
    {
        public SportsService(SportifyDbContext context, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
            : base(context, mapper, userManager, signInManager)
        {
        }

        public IList<SportViewModel> GetAllSports()
        {
            var sports = this.Context
                .Sports
                .OrderBy(s => s.Name)
                .AsQueryable();

            var sportsViewModel = this.Mapper.Map<IQueryable<Sport>, IList<SportViewModel>>(sports);

            return sportsViewModel;
        }

        public Sport Add(AddSportViewModel model)
        {
            var sport = this.Mapper.Map<Sport>(model);

            this.Context.Sports.Add(sport);
            this.Context.SaveChanges();

            return sport;
        }

        public SportViewModel GetSportById(int id)
        {
            var sport = this.Context
                .Sports
                .FirstOrDefault(s => s.Id == id);

            var sportViewModel = this.Mapper.Map<SportViewModel>(sport);

            return sportViewModel;
        }
    }
}