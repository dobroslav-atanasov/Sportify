namespace Sportify.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapping;
    using Data;
    using Data.Models;
    using Data.ViewModels.Countries;
    using Interfaces;

    public class CountriesService : ICountriesService
    {
        private readonly SportifyDbContext context;

        public CountriesService(SportifyDbContext context)
        {
            this.context = context;
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
                .OrderBy(x => x.Name)
                .To<CountrySelectViewModel>();

            return countries;
        }
    }
}