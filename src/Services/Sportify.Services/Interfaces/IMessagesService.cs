namespace Sportify.Services.Interfaces
{
    using Data.ViewModels.Messages;
    using System.Collections.Generic;

    public interface IMessagesService
    {
        bool IsSendMessage(AddMessageViewModel model);

        IEnumerable<MessageViewModel> GetAllMessages();
    }
}