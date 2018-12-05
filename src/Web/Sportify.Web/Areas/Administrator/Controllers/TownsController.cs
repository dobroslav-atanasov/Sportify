using Microsoft.AspNetCore.Mvc;
using Sportify.Data.ViewModels.Towns;
using Sportify.Services.Interfaces;

namespace Sportify.Web.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class TownsController : Controller
    {
        private readonly ICountriesService countriesService;
        private readonly ITownsService townsService;

        public TownsController(ICountriesService countriesService, ITownsService townsService)
        {
            this.countriesService = countriesService;
            this.townsService = townsService;
        }

        public IActionResult Add()
        {
            this.ViewData["Countries"] = this.countriesService.GetAllCountryNames();
            return this.View();
        }

        [HttpPost]
        public IActionResult Add(AddTownViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            this.townsService.AddTown(model);

            return this.RedirectToAction("Index", "Home", new { area = "Administrator" });
        }
    }
}
