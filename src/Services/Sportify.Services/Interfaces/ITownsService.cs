namespace Sportify.Services.Interfaces
{
    using System.Collections.Generic;

    using Data.Models;
    using Data.ViewModels.Towns;

    public interface ITownsService
    {
        Town AddTown(AddTownViewModel model);

        IEnumerable<TownViewModel> GetAllTowns();

        TownViewModel GetTownById(int id);

        bool IsDeleteTown(TownViewModel model);

        IList<int> GetAllTownIdsByCountryId(int id);

        TownViewModel UpdateTown(TownViewModel model);
    }
}