namespace Sportify.Services.Interfaces
{
    using System.Collections.Generic;

    using Data.Models;
    using Data.ViewModels.Sports;

    public interface ISportsService
    {
        IList<SportViewModel> GetAllSports();

        Sport Add(AddSportViewModel model);

        SportViewModel GetSportById(int id);

        SportViewModel UpdateSport(SportViewModel model);
    }
}