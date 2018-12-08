namespace Sportify.Web.Areas.Administrator.Controllers
{
    using Data.ViewModels.Sports;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.Interfaces;
    using X.PagedList;

    [Area("Administrator")]
    public class SportsAdminController : Controller
    {
        private readonly ISportsService sportsService;

        public SportsAdminController(ISportsService sportsService)
        {
            this.sportsService = sportsService;
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult AllSports(int? page)
        {
            var sports = this.sportsService.GetAllSports();

            var pageNumber = page ?? 1;
            var sportsOnPage = sports.ToPagedList(pageNumber, 10);

            return this.View(sportsOnPage);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Add()
        {
            return this.View();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public IActionResult Add(AddSportViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            this.sportsService.Add(model);

            return this.RedirectToAction("AllSports", "SportsAdmin", new { area = "Administrator" });
        }
    }
}