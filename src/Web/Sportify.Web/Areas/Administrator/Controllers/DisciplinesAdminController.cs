namespace Sportify.Web.Areas.Administrator.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.Interfaces;
    using X.PagedList;

    [Area("Administrator")]
    public class DisciplinesAdminController : Controller
    {
        private readonly IDisciplinesService disciplinesService;

        public DisciplinesAdminController(IDisciplinesService disciplinesService)
        {
            this.disciplinesService = disciplinesService;
        }
        
        [Authorize(Roles = "Administrator")]
        public IActionResult AllDisciplines(int? page)
        {
            var disciplines = this.disciplinesService.GetAllDisciplines();

            var pageNumber = page ?? 1;
            var disciplinesOnPage = disciplines.ToPagedList(pageNumber, 10);

            return this.View(disciplinesOnPage);
        }
    }
}