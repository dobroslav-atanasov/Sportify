using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sportify.Constants;
using Sportify.Data.ViewModels.Disciplines;
using Sportify.Services.Interfaces;
using X.PagedList;

namespace Sportify.Web.Areas.Sport.Controllers
{
    [Area(AreaConstants.Sport)]
    public class DisciplinesController : Controller
    {
        private readonly IDisciplinesService disciplinesService;
        private readonly ISportsService sportsService;

        public DisciplinesController(IDisciplinesService disciplinesService, ISportsService sportsService)
        {
            this.disciplinesService = disciplinesService;
            this.sportsService = sportsService;
        }

        [Authorize(Roles = Role.Administrator)]
        public IActionResult All(int? page)
        {
            var disciplines = this.disciplinesService.GetAllDisciplines();

            var pageNumber = page ?? 1;
            var disciplinesOnPage = disciplines.ToPagedList(pageNumber, 10);

            return this.View(disciplinesOnPage);
        }

        [Authorize(Roles = Role.Administrator)]
        public IActionResult Add()
        {
            this.ViewData[GlobalConstants.Sports] = this.sportsService.GetAllSports();
            return this.View();
        }

        [Authorize(Roles = Role.Administrator)]
        [HttpPost]
        public IActionResult Add(AddDisciplineViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.ViewData[GlobalConstants.Sports] = this.sportsService.GetAllSports();
                return this.View(model);
            }

            this.disciplinesService.AddDiscipline(model);

            return this.RedirectToAction("All", "Disciplines", new { area = AreaConstants.Sport });
        }
    }
}
