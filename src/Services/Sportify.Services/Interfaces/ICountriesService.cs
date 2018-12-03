namespace Sportify.Services.Interfaces
{
    using System.Collections.Generic;
    using Data.Models;
    using Data.ViewModels.Countries;

    public interface ICountriesService
    {
        Country GetCountryById(int id);

        Country GetCountryByName(string name);
        
        IEnumerable<CountrySelectViewModel> GetAllCountryNames();
    }
}