namespace Sportify.Services.Interfaces
{
    using System.Collections.Generic;
    using Data.ViewModels.Disciplines;
    using Sportify.Data.Models;

    public interface IDisciplinesService
    {
        IEnumerable<DisciplineViewModel> GetAllDisciplines();

        Discipline AddDiscipline(AddDisciplineViewModel model);
    }
}