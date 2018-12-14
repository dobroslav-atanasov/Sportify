namespace Sportify.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Data.Models;
    using Data.ViewModels.Organizations;
    using global::AutoMapper;
    using Interfaces;
    using Microsoft.AspNetCore.Identity;

    public class OrganizationsService : BaseService, IOrganizationsService
    {
        public OrganizationsService(SportifyDbContext context, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager) 
            : base(context, mapper, userManager, signInManager)
        {
        }

        public Organization Create(CreateOrganizationViewModel model, string username)
        {
            var user = this.UserManager.FindByNameAsync(username).GetAwaiter().GetResult();

            var organization = this.Mapper.Map<Organization>(model);
            organization.President = user;

            this.Context.Organizations.Add(organization);
            this.Context.SaveChanges();

            return organization;
        }

        public IEnumerable<OrganizationViewModel> GetAllOrganizations()
        {
            var organizations = this.Context
                .Organizations
                .OrderBy(o => o.Name)
                .AsQueryable();

            var organizationsViewModel = this.Mapper.Map<IQueryable<Organization>, IEnumerable<OrganizationViewModel>>(organizations);

            return organizationsViewModel;
        }
    }
}