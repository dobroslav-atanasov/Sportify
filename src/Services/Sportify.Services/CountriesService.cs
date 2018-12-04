namespace Sportify.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Data.Models;
    using Data.ViewModels.Countries;
    using global::AutoMapper;
    using Interfaces;

    public class CountriesService : ICountriesService
    {
        private readonly SportifyDbContext context;
        private readonly IMapper mapper;

        public CountriesService(SportifyDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public Country GetCountryById(int id)
        {
            var country = this.context
                .Countries
                .FirstOrDefault(x => x.Id == id);

            return country;
        }

        public Country GetCountryByName(string name)
        {
            var country = this.context
                .Countries
                .FirstOrDefault(x => x.Name == name);

            return country;
        }

        public IEnumerable<CountrySelectViewModel> GetAllCountryNames()
        {
            var countries = this.context
                .Countries
                .OrderBy(x => x.Name);

            var countriesModel = this.mapper.Map<IOrderedQueryable<Country>, IEnumerable<CountrySelectViewModel>>(countries);

            return countriesModel;
        }
    }
}