namespace Sportify.Web.Areas.Sport.Controllers
{
    using Constants;
    using Data.ViewModels.Disciplines;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.Interfaces;
    using X.PagedList;

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

        [HttpPost]
        [Authorize(Roles = Role.Administrator)]
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

        [Authorize(Roles = Role.Administrator)]
        public IActionResult Edit(int id)
        {
            this.ViewData[GlobalConstants.Sports] = this.sportsService.GetAllSports();
            var discipline = this.disciplinesService.GetDisciplineById(id);
            return this.View(discipline);
        }

        [HttpPost]
        [Authorize(Roles = Role.Administrator)]
        public IActionResult Edit(DisciplineViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.ViewData[GlobalConstants.Sports] = this.sportsService.GetAllSports();
                return this.View(model);
            }

            var discipline = this.disciplinesService.UpdateDiscipline(model);
            if (discipline == null)
            {
                this.ViewData[GlobalConstants.Sports] = this.sportsService.GetAllSports();
                this.ViewData[GlobalConstants.Error] = GlobalConstants.DisciplineWasNotUpdated;
                return this.View(model);
            }

            this.ViewData[GlobalConstants.Message] = GlobalConstants.DisciplineWasUpdated;
            this.ViewData[GlobalConstants.Sports] = this.sportsService.GetAllSports();
            return this.View();
        }

        [Authorize(Roles = Role.Administrator)]
        public IActionResult Details(int id)
        {
            var discipline = this.disciplinesService.GetDisciplineById(id);
            return this.View(discipline);
        }

        [Authorize(Roles = Role.Administrator)]
        public IActionResult Delete(int id)
        {
            var discipline = this.disciplinesService.GetDisciplineById(id);
            return this.View(discipline);
        }

        [HttpPost]
        [Authorize(Roles = Role.Administrator)]
        public IActionResult Delete(DisciplineViewModel model)
        {
            this.disciplinesService.DeleteDiscipline(model);
            return this.RedirectToAction("All", "Disciplines", new { area = AreaConstants.Sport });
        }
    }
}