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

        public Message SendMessage(AddMessageViewModel model, User user)
        {
            var message = this.Mapper.Map<Message>(model);
            if (user != null)
            {
                message.UserId = user.Id;
            }

            this.Context.Messages.Add(message);
            this.Context.SaveChanges();

            return message;
        }

        public IEnumerable<MessageViewModel> GetAllMessages()
        {
            // TODO: 
            var messages = this.Context
                .Messages
                .Where(m => m.User != null)
                .OrderBy(m => m.PublishedOn)
                .AsQueryable();

            var messagesViewModel = this.Mapper.Map<IQueryable<Message>, IEnumerable<MessageViewModel>>(messages);
            return messagesViewModel;
        }
    }
}