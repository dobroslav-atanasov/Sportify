namespace Sportify.Services.Interfaces
{
    using System.Collections.Generic;
    using Data.Models;
    using Data.ViewModels.Organizations;

    public interface IOrganizationsService
    {
        Organization Create(CreateOrganizationViewModel model, string username);

        IEnumerable<OrganizationViewModel> GetAllOrganizations();

        OrganizationViewModel GetOrganizationByUser(string username);

        OrganizationViewModel UpdateOrganization(OrganizationViewModel model);

        bool CheckUserHasOrganization(string username);
    }
}