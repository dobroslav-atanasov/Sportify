namespace Sportify.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Data.Models;
    using Data.ViewModels.Messages;
    using global::AutoMapper;
    using Interfaces;
    using Microsoft.AspNetCore.Identity;

    public class MessagesService : BaseService, IMessagesService
    {
        public MessagesService(SportifyDbContext context, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
            : base(context, mapper, userManager, signInManager)
        {
        }

        public bool IsSendMessage(AddMessageViewModel model)
        {
            var user = this.UserManager.FindByNameAsync(model.Username).GetAwaiter().GetResult();
            if (user == null)
            {
                return false;
            }

            var message = this.Mapper.Map<Message>(model);
            message.UserId = user.Id;

            this.Context.Messages.Add(message);
            this.Context.SaveChanges();

            return true;
        }

        public IEnumerable<MessageViewModel> GetAllMessages()
        {
            var messages = this.Context
                .Messages
                .OrderBy(m => m.PublishedOn)
                .AsQueryable();

            var messagesViewModel = this.Mapper.Map<IQueryable<Message>, IEnumerable<MessageViewModel>>(messages);
            return messagesViewModel;
        }
    }
}