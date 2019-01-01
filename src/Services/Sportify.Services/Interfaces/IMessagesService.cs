namespace Sportify.Services.Interfaces
{
    using System.Collections.Generic;

    using Data.Models;
    using Data.ViewModels.Messages;

    public interface IMessagesService
    {
        Message Send(SendMessageViewModel model, User user);

        IEnumerable<MessageViewModel> GetAllMessages();
    }
}