namespace Sportify.AutoMapper
{
    using Data.Models;
    using Data.ViewModels.Countries;
    using Data.ViewModels.Disciplines;
    using Data.ViewModels.Sports;
    using Data.ViewModels.Towns;
    using Data.ViewModels.Users;
    using global::AutoMapper;

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            this.CreateMap<Country, CountrySelectViewModel>().ReverseMap();

            this.CreateMap<User, CreateAccountViewModel>().ReverseMap();

            this.CreateMap<SignInViewModel, User>()
                .ForMember(u => u.UserName, svm => svm.MapFrom(x => x.Username))
                .ReverseMap();

            this.CreateMap<Town, AddTownViewModel>().ReverseMap();

            this.CreateMap<Town, TownViewModel>()
                .ForMember(tvm => tvm.CountryName, t => t.MapFrom(x => x.Country.Name))
                .ReverseMap();

            this.CreateMap<User, UserAdminViewModel>()
                .ForMember(uvm => uvm.Id, u => u.MapFrom(x => x.Id))
                .ForMember(uvm => uvm.Username, u => u.MapFrom(x => x.UserName))
                .ReverseMap();

            this.CreateMap<Sport, SportViewModel>().ReverseMap();

            this.CreateMap<Sport, AddSportViewModel>().ReverseMap();

            this.CreateMap<Discipline, DisciplineViewModel>().ReverseMap();

            this.CreateMap<Discipline, AddDisciplineViewModel>().ReverseMap();
        }
    }
}