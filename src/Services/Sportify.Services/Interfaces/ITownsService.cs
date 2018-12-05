namespace Sportify.Services.Interfaces
{
    using Data.ViewModels.Towns;

    public interface ITownsService
    {
        void AddTown(AddTownViewModel model);
    }
}