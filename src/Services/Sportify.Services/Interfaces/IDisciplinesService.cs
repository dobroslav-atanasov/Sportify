namespace Sportify.Services.Interfaces
{
    using System.Collections.Generic;
    using Data.ViewModels.Disciplines;

    public interface IDisciplinesService
    {
        IEnumerable<DisciplineViewModel> GetAllDisciplines();

        void AddDiscipline(AddDisciplineViewModel model);
    }
}