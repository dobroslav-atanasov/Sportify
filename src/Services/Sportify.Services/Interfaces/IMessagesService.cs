namespace Sportify.Services.Interfaces
{
    using Data.ViewModels.Messages;

    public interface IMessagesService
    {
        bool IsSendMessage(AddMessageViewModel model);
    }
}