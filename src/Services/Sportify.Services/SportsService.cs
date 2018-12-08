namespace Sportify.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Data.Models;
    using Data.ViewModels.Sports;
    using global::AutoMapper;
    using Interfaces;

    public class SportsService : ISportsService
    {
        private readonly SportifyDbContext context;
        private readonly IMapper mapper;

        public SportsService(SportifyDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IEnumerable<SportViewModel> GetAllSports()
        {
            var sports = this.context
                .Sports
                .OrderBy(s => s.Name)
                .AsQueryable();

            var sportsViewModel = this.mapper.Map<IQueryable<Sport>, IEnumerable<SportViewModel>>(sports);

            return sportsViewModel;
        }
    }
}