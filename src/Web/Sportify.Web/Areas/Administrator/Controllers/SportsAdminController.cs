namespace Sportify.Web.Areas.Administrator.Controllers
{
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

        public IActionResult AllSports(int? page)
        {
            var sports = this.sportsService.GetAllSports();

            var pageNumber = page ?? 1;
            var sportsOnPage = sports.ToPagedList(pageNumber, 10);

            return this.View(sportsOnPage);
        }
    }
}