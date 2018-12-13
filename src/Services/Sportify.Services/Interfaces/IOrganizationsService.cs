namespace Sportify.Services.Interfaces
{
    using System.Collections.Generic;
    using Data.ViewModels.Organizations;

    public interface IOrganizationsService
    {
        void Create(CreateOrganizationViewModel model, string username);

        IEnumerable<OrganizationViewModel> GetAllOrganizations();
    }
}