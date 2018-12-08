﻿namespace Sportify.Web.Areas.Administrator.Controllers
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
            var users = this.usersService.GetAllUsers();

            var pageNumber = page ?? 1;
            var usersOnPage = users.ToPagedList(pageNumber, 10);

            return this.View(usersOnPage);
        }
    }
}