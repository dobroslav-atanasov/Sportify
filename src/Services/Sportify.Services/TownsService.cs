namespace Sportify.Services
{
    using Data;
    using Data.Models;
    using Data.ViewModels.Towns;
    using global::AutoMapper;
    using Interfaces;

    public class TownsService : ITownsService
    {
        private readonly SportifyDbContext context;
        private readonly ICountriesService countriesService;
        private readonly IMapper mapper;

        public TownsService(SportifyDbContext context, ICountriesService countriesService, IMapper mapper)
        {
            this.context = context;
            this.countriesService = countriesService;
            this.mapper = mapper;
        }

        public void AddTown(AddTownViewModel model)
        {
            var town = this.mapper.Map<Town>(model);

            this.context.Towns.Add(town);
            this.context.SaveChanges();
        }
    }
}