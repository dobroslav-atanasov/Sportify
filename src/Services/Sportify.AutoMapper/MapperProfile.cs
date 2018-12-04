namespace Sportify.AutoMapper
{
    using Data.Models;
    using Data.ViewModels.Countries;
    using global::AutoMapper;

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            this.CreateMap<Country, CountrySelectViewModel>().ReverseMap();
        }
    }
}