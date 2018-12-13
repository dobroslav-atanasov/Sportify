using Sportify.Data.Models;
using Sportify.Data.ViewModels.Organizations;

namespace Sportify.Services.Interfaces
{
    public interface IOrganizationsService
    {
        void Create(CreateOrganizationViewModel model, string username);
    }
}
