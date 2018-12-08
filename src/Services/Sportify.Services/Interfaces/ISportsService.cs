namespace Sportify.Services.Interfaces
{
    using System.Collections.Generic;
    using Data.ViewModels.Sports;

    public interface ISportsService
    {
        IEnumerable<SportViewModel> GetAllSports();

        void Add(AddSportViewModel model);
    }
}