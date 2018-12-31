namespace Sportify.AutoMapper
{
    using System;
    using System.Globalization;

    using Constants;
    using Data.Models;
    using Data.ViewModels.Countries;
    using Data.ViewModels.Disciplines;
    using Data.ViewModels.Events;
    using Data.ViewModels.Messages;
    using Data.ViewModels.Organizations;
    using Data.ViewModels.Participants;
    using Data.ViewModels.Results;
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
            this.CreateMap<Discipline, DisciplineViewModel>()
                .ForMember(dvm => dvm.Sport, d => d.MapFrom(x => x.Sport.Name)).ReverseMap();

            this.CreateMap<Discipline, AddDisciplineViewModel>().ReverseMap();

            // Events
            this.CreateMap<Event, CreateEventViewModel>()
                .ForMember(evm => evm.DisciplineId, e => e.MapFrom(x => x.DisciplineId))
                .ReverseMap();

            this.CreateMap<Event, UpdateEventViewModel>().ReverseMap();

            this.CreateMap<Event, EventViewModel>()
                .ForMember(evm => evm.Date, e => e.MapFrom(x => x.Date.ToString(GlobalConstants.DateToString, CultureInfo.InvariantCulture)))
                .ForMember(evm => evm.Time, e => e.MapFrom(x => x.Date.ToString(GlobalConstants.TimeToString)))
                .ForMember(evm => evm.ImageVenueUrl, e => e.MapFrom(x => x.Venue.ImageVenueUrl))
                .ForMember(evm => evm.Abbreviation, e => e.MapFrom(x => x.Organization.Abbreviation))
                .ForMember(evm => evm.Organization, e => e.MapFrom(x => x.Organization.Name))
                .ForMember(evm => evm.Sport, e => e.MapFrom(x => x.Discipline.Sport.Name))
                .ForMember(evm => evm.Discipline, e => e.MapFrom(x => x.Discipline.Name))
                .ForMember(evm => evm.Town, e=> e.MapFrom(x => x.Venue.Town.Name))
                .ForMember(evm => evm.Venue, e => e.MapFrom(x => x.Venue.Name))
                .ForMember(evm => evm.RealDate, e => e.MapFrom(x => x.Date))
                .ReverseMap();
            
            // Messages
            this.CreateMap<Message, SendMessageViewModel>().ReverseMap();
            
            this.CreateMap<Message, MessageViewModel>()
                .ForMember(mvm => mvm.Username, m => m.MapFrom(x => x.User.UserName))
                .ForMember(mvm => mvm.PublishedOn, m => m.MapFrom(x => x.PublishedOn.ToString(GlobalConstants.PublishedOn)))
                .ReverseMap();

            // Organizations
            this.CreateMap<Organization, CreateOrganizationViewModel>().ReverseMap();

            this.CreateMap<Organization, OrganizationViewModel>()
                .ForMember(ovm => ovm.President, o => o.MapFrom(x => $"{x.President.FirstName} {x.President.LastName}"))
                .ReverseMap();

            // Participants
            this.CreateMap<Participant, ParticipantViewModel>().ReverseMap();

            this.CreateMap<Participant, MyEventViewModel>()
                .ForMember(evm => evm.EventName, p => p.MapFrom(x => x.Event.EventName))
                .ForMember(evm => evm.Date, p => p.MapFrom(x => x.Event.Date.ToString(GlobalConstants.ShortDateToString, CultureInfo.InvariantCulture)))
                .ForMember(evm => evm.Time, p => p.MapFrom(x => x.Event.Date.ToString(GlobalConstants.TimeToString)))
                .ForMember(evm => evm.Sport, p => p.MapFrom(x => x.Event.Discipline.Sport.Name))
                .ForMember(evm => evm.Discipline, p => p.MapFrom(x => x.Event.Discipline.Name))
                .ForMember(evm => evm.Town, p => p.MapFrom(x => x.Event.Venue.Town.Name))
                .ForMember(evm => evm.Venue, p => p.MapFrom(x => x.Event.Venue.Name))
                .ForMember(evm => evm.RemainingTime, p => p.MapFrom(x => $"{Math.Ceiling((x.Event.Date - DateTime.UtcNow).TotalDays)}{GlobalConstants.RemainingDays}"))
                .ReverseMap();

            // Results
            this.CreateMap<Participant, MyResultViewModel>()
                .ForMember(rvm => rvm.EventName, p => p.MapFrom(x => x.Event.EventName))
                .ForMember(rvm => rvm.Date, p => p.MapFrom(x => x.Event.Date.ToString(GlobalConstants.ShortDateToString, CultureInfo.InvariantCulture)))
                .ForMember(rvm => rvm.Time, p => p.MapFrom(x => x.Event.Date.ToString(GlobalConstants.TimeToString)))
                .ForMember(rvm => rvm.Sport, p => p.MapFrom(x => x.Event.Discipline.Sport.Name))
                .ForMember(rvm => rvm.Discipline, p => p.MapFrom(x => x.Event.Discipline.Name))
                .ForMember(rvm => rvm.Result, p => p.MapFrom(x => x.Result.Value.ToString(GlobalConstants.Result, CultureInfo.InvariantCulture)))
                .ForMember(rvm => rvm.Venue, p => p.MapFrom(x => x.Event.Venue.Name))
                .ReverseMap();

            this.CreateMap<Participant, EventResultViewModel>()
                .ForMember(evm => evm.Username, p => p.MapFrom(x => x.User.UserName))
                .ForMember(evm => evm.FullName, p => p.MapFrom(x => $"{x.User.FirstName} {x.User.LastName}"))
                .ForMember(evm => evm.Country, p => p.MapFrom(x => x.User.Country.Name))
                .ForMember(evm => evm.Result, p => p.MapFrom(x => x.Result.Value.ToString(GlobalConstants.Result, CultureInfo.InvariantCulture)))
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
                .ForMember(vvm => vvm.Country, v => v.MapFrom(x => x.Town.Country.Name))
                .ReverseMap();
        }
    }
}