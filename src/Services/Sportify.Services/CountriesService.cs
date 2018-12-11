namespace Sportify.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Data.Models;
    using Data.ViewModels.Countries;
    using global::AutoMapper;
    using Interfaces;
    using Microsoft.AspNetCore.Identity;

    public class CountriesService : BaseService, ICountriesService
    {
        public CountriesService(SportifyDbContext context, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
            : base(context, mapper, userManager, signInManager)
        {
        }

        public Country GetCountryById(int id)
        {
            var country = this.Context
                .Countries
                .FirstOrDefault(x => x.Id == id);

            return country;
        }

        public Country GetCountryByName(string name)
        {
            var country = this.Context
                .Countries
                .FirstOrDefault(x => x.Name == name);

            return country;
        }

        public IEnumerable<CountrySelectViewModel> GetAllCountryNames()
        {
            var countries = this.Context
                .Countries
                .OrderBy(x => x.Name);

            var countriesModel = this.Mapper.Map<IOrderedQueryable<Country>, IEnumerable<CountrySelectViewModel>>(countries);

            return countriesModel;
        }
    }
}