using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sportify.Constants;

namespace Sportify.Web.Controllers
{
    public class VenuesController : Controller
    {
        [Authorize(Roles = "Administrator, Editor")]
        public IActionResult AddVenue()
        {
            return this.View();
        }
    }
}
