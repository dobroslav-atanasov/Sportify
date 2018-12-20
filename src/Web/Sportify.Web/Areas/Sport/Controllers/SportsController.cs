namespace Sportify.Web.Areas.Sport.Controllers
{
    using Constants;
    using Data.ViewModels.Sports;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.Interfaces;
    using X.PagedList;

    [Area(AreaConstants.Sport)]
    public class SportsController : Controller
    {
        private readonly ISportsService sportsService;
        private readonly IDisciplinesService disciplinesService;

        public SportsController(ISportsService sportsService, IDisciplinesService disciplinesService)
        {
            this.sportsService = sportsService;
            this.disciplinesService = disciplinesService;
        }

        [Authorize(Roles = Role.Administrator)]
        public IActionResult All(int? page)
        {
            var sports = this.sportsService.GetAllSports();

            var pageNumber = page ?? 1;
            var sportsOnPage = sports.ToPagedList(pageNumber, 10);

            return this.View(sportsOnPage);
        }

        [Authorize(Roles = Role.Administrator)]
        public IActionResult Add()
        {
            return this.View();
        }

        [Authorize(Roles = Role.Administrator)]
        [HttpPost]
        public IActionResult Add(AddSportViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            this.sportsService.Add(model);

            return this.RedirectToAction("All", "Sports", new { area = AreaConstants.Sport });
        }

        public IActionResult AllSports()
        {
            var sports = this.sportsService.GetAllSportsInfo();
            return this.View(sports);
        }

        public IActionResult Details(int id)
        {
            var sport = this.sportsService.GetSportById(id);
            this.ViewData[GlobalConstants.Disciplines] = this.disciplinesService.GetDisciplinesBySportId(id);
            return this.View(sport);
        }
    }
}