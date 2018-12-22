namespace Sportify.Services.Interfaces
{
    using System.Collections.Generic;
    using Data.ViewModels.Sports;
    using Sportify.Data.Models;

    public interface ISportsService
    {
        IList<SportViewModel> GetAllSports();

        Sport Add(AddSportViewModel model);

        SportViewModel GetSportById(int id);

        SportViewModel UpdateSport(SportViewModel model);
    }
}