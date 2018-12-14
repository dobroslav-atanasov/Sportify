namespace Sportify.Services.Interfaces
{
    using System.Collections.Generic;
    using Data.ViewModels.Organizations;
    using Sportify.Data.Models;

    public interface IOrganizationsService
    {
        Organization Create(CreateOrganizationViewModel model, string username);

        IEnumerable<OrganizationViewModel> GetAllOrganizations();
    }
}