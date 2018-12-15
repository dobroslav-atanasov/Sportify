namespace Sportify.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Linq;
    using Data.ViewModels.Towns;
    using Sportify.Data.Models;

    public interface ITownsService
    {
        Town AddTown(AddTownViewModel model);

        IEnumerable<TownViewModel> GetAllTowns();

        TownViewModel GetTownById(int id);

        bool IsDeleteTown(TownViewModel model);

        IList<int> GetAllTownIdsByCountryId(int id);
    }
}