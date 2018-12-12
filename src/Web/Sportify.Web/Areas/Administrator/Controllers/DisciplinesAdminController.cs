namespace Sportify.Web.Areas.Administrator.Controllers
{
    using Constants;
    using Data.ViewModels.Disciplines;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.Interfaces;
    using X.PagedList;

    [Area("Administrator")]
    public class DisciplinesAdminController : Controller
    {
        private readonly IDisciplinesService disciplinesService;
        private readonly ISportsService sportsService;

        public DisciplinesAdminController(IDisciplinesService disciplinesService, ISportsService sportsService)
        {
            this.disciplinesService = disciplinesService;
            this.sportsService = sportsService;
        }

        [Authorize(Roles = Constants.AdministratorRole)]
        public IActionResult AllDisciplines(int? page)
        {
            var disciplines = this.disciplinesService.GetAllDisciplines();

            var pageNumber = page ?? 1;
            var disciplinesOnPage = disciplines.ToPagedList(pageNumber, 10);

            return this.View(disciplinesOnPage);
        }


        [Authorize(Roles = Constants.AdministratorRole)]
        public IActionResult Add()
        {
            this.ViewData["Sports"] = this.sportsService.GetAllSports();
            return this.View();
        }


        [Authorize(Roles = Constants.AdministratorRole)]
        [HttpPost]
        public IActionResult Add(AddDisciplineViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.ViewData["Sports"] = this.sportsService.GetAllSports();
                return this.View(model);
            }

            this.disciplinesService.AddDiscipline(model);

            return this.RedirectToAction("AllDisciplines", "DisciplinesAdmin", new { area = "Administrator" });
        }
    }
}