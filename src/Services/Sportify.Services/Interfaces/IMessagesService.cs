namespace Sportify.Services.Interfaces
{
    using Data.ViewModels.Messages;
    using Sportify.Data.Models;
    using System.Collections.Generic;

    public interface IMessagesService
    {
        Message SendMessage(AddMessageViewModel model, User user);

        IEnumerable<MessageViewModel> GetAllMessages();
    }
}