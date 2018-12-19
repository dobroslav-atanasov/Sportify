namespace Sportify.AutoMapper
{
    using System.Globalization;
    using Data.Models;
    using Data.ViewModels.Countries;
    using Data.ViewModels.Disciplines;
    using Data.ViewModels.Events;
    using Data.ViewModels.Messages;
    using Data.ViewModels.Organizations;
    using Data.ViewModels.Sports;
    using Data.ViewModels.Towns;
    using Data.ViewModels.Users;
    using Data.ViewModels.Venues;
    using global::AutoMapper;

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // Countries
            this.CreateMap<Country, CountrySelectViewModel>().ReverseMap();

            // Disciplines
            this.CreateMap<Discipline, DisciplineViewModel>().ReverseMap();

            this.CreateMap<Discipline, AddDisciplineViewModel>().ReverseMap();

            // Events
            this.CreateMap<Event, CreateEventViewModel>()
                .ForMember(evm => evm.DisciplineId, e => e.MapFrom(x => x.DisciplineId))
                .ReverseMap();

            this.CreateMap<Event, EventViewModel>()
                .ForMember(evm => evm.Date, e => e.MapFrom(x => x.Date.ToString($"dd MMMM yyyy, dddd", CultureInfo.InvariantCulture)))
                .ForMember(evm => evm.Time, e => e.MapFrom(x => x.Date.ToString("HH:mm")))
                .ForMember(evm => evm.ImageVenueUrl, e => e.MapFrom(x => x.Venue.ImageVenueUrl))
                .ForMember(evm => evm.Organization, e => e.MapFrom(x => x.Organization.Name))
                .ForMember(evm => evm.Sport, e => e.MapFrom(x => x.Discipline.Sport.Name))
                .ForMember(evm => evm.Discipline, e => e.MapFrom(x => x.Discipline.Name))
                .ForMember(evm => evm.Town, e=> e.MapFrom(x => x.Venue.Town.Name))
                .ForMember(evm => evm.Venue, e => e.MapFrom(x => x.Venue.Name))
                .ReverseMap();
            
            // Messages
            this.CreateMap<Message, SendMessageViewModel>().ReverseMap();
            
            this.CreateMap<Message, MessageViewModel>()
                .ForMember(mvm => mvm.Username, m => m.MapFrom(x => x.User.UserName))
                .ForMember(mvm => mvm.PublishedOn, m => m.MapFrom(x => x.PublishedOn.ToString("dd-MM-yyyy HH:mm:ss")))
                .ReverseMap();

            // Organizations
            this.CreateMap<Organization, CreateOrganizationViewModel>().ReverseMap();

            this.CreateMap<Organization, OrganizationViewModel>()
                .ForMember(ovm => ovm.President, o => o.MapFrom(x => x.President.UserName))
                .ReverseMap();
            
            // Sports
            this.CreateMap<Sport, SportViewModel>().ReverseMap();

            this.CreateMap<Sport, AddSportViewModel>().ReverseMap();

            // Towns
            this.CreateMap<Town, AddTownViewModel>().ReverseMap();

            this.CreateMap<Town, TownViewModel>()
                .ForMember(tvm => tvm.CountryName, t => t.MapFrom(x => x.Country.Name))
                .ReverseMap();

            // Users
            this.CreateMap<User, CreateAccountViewModel>().ReverseMap();

            this.CreateMap<SignInViewModel, User>()
                .ForMember(u => u.UserName, svm => svm.MapFrom(x => x.Username))
                .ReverseMap();

            this.CreateMap<User, UserAdminViewModel>()
                .ForMember(uvm => uvm.Id, u => u.MapFrom(x => x.Id))
                .ForMember(uvm => uvm.Username, u => u.MapFrom(x => x.UserName))
                .ReverseMap();

            this.CreateMap<UpdateAccountViewModel, User>()
                .ForMember(u => u.UserName, pvm => pvm.MapFrom(x => x.Username))
                .ReverseMap();

            // Venues
            this.CreateMap<Venue, AddVenueViewModel>().ReverseMap();

            this.CreateMap<Venue, VenueViewModel>()
                .ForMember(vvm => vvm.Town, v => v.MapFrom(x => x.Town.Name))
                .ReverseMap();
        }
    }
}