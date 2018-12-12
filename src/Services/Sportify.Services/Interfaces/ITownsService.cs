namespace Sportify.Services.Interfaces
{
    using System.Collections.Generic;
    using Data.ViewModels.Towns;
    using Sportify.Data.Models;

    public interface ITownsService
    {
        void AddTown(AddTownViewModel model);

        IEnumerable<TownViewModel> GetAllTowns();

        TownViewModel GetTownById(int id);

        bool IsDeleteTown(TownViewModel model);
    }
}