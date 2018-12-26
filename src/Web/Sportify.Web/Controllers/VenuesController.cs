namespace Sportify.Web.Controllers
{
    using Constants;
    using Data.ViewModels.Countries;
    using Data.ViewModels.Venues;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.Interfaces;
    using X.PagedList;

    public class VenuesController : Controller
    {
        private readonly IVenuesService venuesService;
        private readonly ITownsService townsService;
        private readonly ICountriesService countriesService;

        public VenuesController(IVenuesService venuesService, ITownsService townsService, ICountriesService countriesService)
        {
            this.venuesService = venuesService;
            this.townsService = townsService;
            this.countriesService = countriesService;
        }

        [Authorize(Roles = Role.AdministratorAndEditor)]
        public IActionResult All(int? page)
        {
            var venues = this.venuesService.GetAllVenues();

            var pageNumber = page ?? 1;
            var venuesOnPage = venues.ToPagedList(pageNumber, 10);

            return this.View(venuesOnPage);
        }

        [Authorize(Roles = Role.AdministratorAndEditor)]
        public IActionResult Add()
        {
            this.ViewData[GlobalConstants.Towns] = this.townsService.GetAllTowns();
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = Role.AdministratorAndEditor)]
        public IActionResult Add(AddVenueViewModel model)
        {
            this.ViewData[GlobalConstants.Towns] = this.townsService.GetAllTowns();
            this.venuesService.AddVenue(model);
            return this.RedirectToAction("All", "Venues", new { area = AreaConstants.Base });
        }

        [Authorize(Roles = Role.AdministratorAndEditor)]
        public IActionResult Edit(int id)
        {
            this.ViewData[GlobalConstants.Towns] = this.townsService.GetAllTowns();
            var venue = this.venuesService.GetVenueById(id);
            return this.View(venue);
        }

        [HttpPost]
        [Authorize(Roles = Role.AdministratorAndEditor)]
        public IActionResult Edit(VenueViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.ViewData[GlobalConstants.Towns] = this.townsService.GetAllTowns();
                return this.View(model);
            }

            var venue = this.venuesService.UpdateVenue(model);
            if (venue == null)
            {
                this.ViewData[GlobalConstants.Towns] = this.townsService.GetAllTowns();
                this.ViewData[GlobalConstants.Error] = GlobalConstants.VenueWasNotUpdated;
                return this.View(model);
            }

            this.ViewData[GlobalConstants.Message] = GlobalConstants.VenueWasUpdated;
            this.ViewData[GlobalConstants.Towns] = this.townsService.GetAllTowns();
            return this.View();
        }

        [Authorize(Roles = Role.AdministratorAndEditor)]
        public IActionResult Details(int id)
        {
            var venue = this.venuesService.GetVenueById(id);
            return this.View(venue);
        }

        [Authorize(Roles = Role.AdministratorAndEditor)]
        public IActionResult Delete(int id)
        {
            var venue = this.venuesService.GetVenueById(id);
            return this.View(venue);
        }

        [HttpPost]
        [Authorize(Roles = Role.Administrator)]
        public IActionResult Delete(VenueViewModel model)
        {
            this.venuesService.DeleteVenue(model);
            return this.RedirectToAction("All", "Venues", new { area = AreaConstants.Base });
        }

        public IActionResult AllVenues()
        {
            this.ViewData[GlobalConstants.Countries] = this.countriesService.GetAllCountryNames();
            return this.View();
        }

        [HttpPost]
        public IActionResult AllVenues(SearchCountryViewModel model)
        {
            this.ViewData[GlobalConstants.Countries] = this.countriesService.GetAllCountryNames();
            this.ViewData[GlobalConstants.Venues] = this.venuesService.GetAllVenuesByCountryId(model.CountryId);
            return this.View();
        }
    }
}