using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sportify.Data.ViewModels.Venues;

namespace Sportify.Web.Controllers
{
    public class VenuesController : Controller
    {
        [Authorize(Roles = "Administrator, Editor")]
        public IActionResult AddVenue()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Editor")]
        public IActionResult AddVenue(AddVenueViewModel model)
        {
            return this.View();
        }
    }
}
