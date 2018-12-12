namespace Sportify.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class EventsController : Controller
    {
        [Authorize(Roles = Constants.Constants.EditorRole)]
        public IActionResult Create()
        {
            return this.View();
        }
    }
}