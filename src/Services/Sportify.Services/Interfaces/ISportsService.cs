namespace Sportify.Services.Interfaces
{
    using System.Collections.Generic;
    using Data.ViewModels.Sports;
    using Sportify.Data.Models;

    public interface ISportsService
    {
        IEnumerable<SportViewModel> GetAllSports();

        Sport Add(AddSportViewModel model);
    }
}