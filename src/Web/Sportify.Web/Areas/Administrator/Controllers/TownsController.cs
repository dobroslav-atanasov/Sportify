namespace Sportify.Web.Areas.Administrator.Controllers
{
    using Constants;
    using Data.ViewModels.Towns;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.Interfaces;
    using X.PagedList;

    [Area(AreaConstants.Administrator)]
    public class TownsController : Controller
    {
        private readonly ICountriesService countriesService;
        private readonly ITownsService townsService;

        public TownsController(ICountriesService countriesService, ITownsService townsService)
        {
            this.countriesService = countriesService;
            this.townsService = townsService;
        }

        [Authorize(Roles = Role.Administrator)]
        public IActionResult All(int? page)
        {
            var towns = this.townsService.GetAllTowns();

            var pageNumber = page ?? 1;
            var townsOnPage = towns.ToPagedList(pageNumber, 10);

            return this.View(townsOnPage);
        }

        [Authorize(Roles = Role.Administrator)]
        public IActionResult Add()
        {
            this.ViewData[GlobalConstants.Countries] = this.countriesService.GetAllCountryNames();
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = Role.Administrator)]
        public IActionResult Add(AddTownViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            this.townsService.AddTown(model);

            return this.RedirectToAction("All", "Towns", new {area = AreaConstants.Administrator});
        }

        [Authorize(Roles = Role.Administrator)]
        public IActionResult Edit(int id)
        {
            var townViewModel = this.townsService.GetTownById(id);
            this.ViewData[GlobalConstants.Countries] = this.countriesService.GetAllCountryNames();

            return this.View(townViewModel);
        }

        [HttpPost]
        [Authorize(Roles = Role.Administrator)]
        public IActionResult Edit(TownViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.ViewData[GlobalConstants.Countries] = this.countriesService.GetAllCountryNames();
                return this.View(model);
            }

            var town = this.townsService.UpdateTown(model);
            if (town == null)
            {
                this.ViewData[GlobalConstants.Countries] = this.countriesService.GetAllCountryNames();
                this.ViewData[GlobalConstants.Error] = GlobalConstants.TownWasNotUpdated;
                return this.View(model);
            }

            this.ViewData[GlobalConstants.Countries] = this.countriesService.GetAllCountryNames();
            this.ViewData[GlobalConstants.Message] = GlobalConstants.TownWasUpdated;
            return this.View();
        }

        [Authorize(Roles = Role.Administrator)]
        public IActionResult Delete(int id)
        {
            var townViewModel = this.townsService.GetTownById(id);
            return this.View(townViewModel);
        }

        [HttpPost]
        [Authorize(Roles = Role.Administrator)]
        public IActionResult Delete(TownViewModel model)
        {
            var isDelete = this.townsService.IsDeleteTown(model);
            if (!isDelete)
            {
                return this.View(model);
            }

            return this.RedirectToAction("All", "Towns", new { area = AreaConstants.Administrator });
        }
    }
}