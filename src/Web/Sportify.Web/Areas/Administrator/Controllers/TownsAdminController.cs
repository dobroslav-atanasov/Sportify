namespace Sportify.Web.Areas.Administrator.Controllers
{
    using Data.ViewModels.Towns;
    using Microsoft.AspNetCore.Mvc;
    using Services.Interfaces;

    [Area("Administrator")]
    public class TownsAdminController : Controller
    {
        private readonly ICountriesService countriesService;
        private readonly ITownsService townsService;

        public TownsAdminController(ICountriesService countriesService, ITownsService townsService)
        {
            this.countriesService = countriesService;
            this.townsService = townsService;
        }

        public IActionResult AllTowns()
        {
            var towns = this.townsService.GetAllTowns();
            return this.View(towns);
        }

        public IActionResult Add()
        {
            this.ViewData["Countries"] = this.countriesService.GetAllCountryNames();
            return this.View();
        }

        [HttpPost]
        public IActionResult Add(AddTownViewModel model)
        {
            if (!this.ModelState.IsValid) return this.View(model);

            this.townsService.AddTown(model);

            return this.RedirectToAction("AllTowns", "TownsAdmin", new {area = "Administrator"});
        }

        public IActionResult Delete(int id)
        {
            var townViewModel = this.townsService.GetTownById(id);
            return this.View(townViewModel);
        }

        [HttpPost]
        public IActionResult Delete(TownViewModel model)
        {
            var isDelete = this.townsService.IsDeleteTown(model);
            if (!isDelete) return this.View(model);

            return this.RedirectToAction("AllTowns", "TownsAdmin", new {area = "Administrator"});
        }

        public IActionResult Edit(int id)
        {
            var townViewModel = this.townsService.GetTownById(id);
            return this.View(townViewModel);
        }

        public IActionResult Details(int id)
        {
            var townViewModel = this.townsService.GetTownById(id);
            return this.View(townViewModel);
        }
    }
}