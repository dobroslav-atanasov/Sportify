namespace Sportify.Services.Interfaces
{
    using System.Collections.Generic;
    using Data.Models;
    using Data.ViewModels.Disciplines;

    public interface IDisciplinesService
    {
        IEnumerable<DisciplineViewModel> GetAllDisciplines();

        Discipline AddDiscipline(AddDisciplineViewModel model);

        IEnumerable<DisciplineViewModel> GetDisciplinesBySportId(int id);

        DisciplineViewModel GetDisciplineById(int id);

        DisciplineViewModel UpdateDiscipline(DisciplineViewModel model);

        void DeleteDiscipline(DisciplineViewModel model);
    }
}