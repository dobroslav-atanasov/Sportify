namespace Sportify.Data.ViewModels.Countries
{
    using AutoMapping.Interfaces;
    using Models;

    public class CountrySelectViewModel : IMapFrom<Country>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}