﻿namespace Sportify.Services
{
    using Data;
    using Data.Models;
    using Data.ViewModels.Messages;
    using global::AutoMapper;
    using Interfaces;
    using Microsoft.AspNetCore.Identity;

    public class MessagesService : IMessagesService
    {
        private readonly SportifyDbContext context;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public MessagesService(SportifyDbContext context, UserManager<User> userManager, IMapper mapper)
        {
            this.context = context;
            this.userManager = userManager;
            this.mapper = mapper;
        }
        
        public bool IsSendMessage(AddMessageViewModel model)
        {
            var user = this.userManager.FindByNameAsync(model.Username).GetAwaiter().GetResult();
            if (user == null)
            {
                return false;
            }

            var message = this.mapper.Map<Message>(model);
            message.UserId = user.Id;

            this.context.Messages.Add(message);
            this.context.SaveChanges();

            return true;
        }
    }
}