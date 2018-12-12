namespace Sportify.Web.Controllers
{
    using Data.ViewModels.Venues;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.Interfaces;

    public class VenuesController : Controller
    {
        private readonly IVenuesService venuesService;
        private readonly ITownsService townsService;

        public VenuesController(IVenuesService venuesService, ITownsService townsService)
        {
            this.venuesService = venuesService;
            this.townsService = townsService;
        }

        [Authorize(Roles = "Administrator, Editor")]
        public IActionResult AllVenues()
        {
            var venues = this.venuesService.GetAllVenues();
            return this.View(venues);
        }

        [Authorize(Roles = "Administrator, Editor")]
        public IActionResult AddVenue()
        {
            this.ViewData["Towns"] = this.townsService.GetAllTowns();
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Editor")]
        public IActionResult AddVenue(AddVenueViewModel model)
        {
            this.ViewData["Towns"] = this.townsService.GetAllTowns();
            this.venuesService.AddVenue(model);
            return this.RedirectToAction("AllVenues", "Venues");
        }
    }
}