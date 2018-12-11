using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sportify.Services.Interfaces;
using X.PagedList;

namespace Sportify.Web.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class MessagesAdminController : Controller
    {
        private readonly IMessagesService messagesService;

        public MessagesAdminController(IMessagesService messagesService)
        {
            this.messagesService = messagesService;
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult AllMessages(int? page)
        {
            var users = this.messagesService.GetAllMessages();

            var pageNumber = page ?? 1;
            var usersOnPage = users.ToPagedList(pageNumber, 10);

            this.ViewData["Messages"] = usersOnPage;
            return this.View();
        }
    }
}