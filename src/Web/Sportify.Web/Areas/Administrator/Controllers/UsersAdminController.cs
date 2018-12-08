namespace Sportify.Web.Areas.Administrator.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.Interfaces;
    using X.PagedList;

    [Area("Administrator")]
    public class UsersAdminController : Controller
    {
        private readonly IUsersService usersService;

        public UsersAdminController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult AllUsers(int? page)
        {
            var usersViewModel = this.usersService.GetAllUsers();

            var pageNumber = page ?? 1;
            var itemsOnPage = usersViewModel.ToPagedList(pageNumber, 2);

            return this.View(itemsOnPage);
        }
    }
}