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

        public OrganizationViewModel GetOrganizationByUser(string username)
        {
            var organization = this.Context
                .Organizations
                .FirstOrDefault(o => o.President.UserName == username);

            if (organization == null)
            {
                return null;
            }

            var organizationViewModel = this.Mapper.Map<OrganizationViewModel>(organization);

            return organizationViewModel;
        }

        public OrganizationViewModel UpdateOrganization(OrganizationViewModel model)
        {
            var organization = this.Context
                .Organizations
                .FirstOrDefault(o => o.Id == model.Id);

            if (organization == null)
            {
                return null;
            }

            organization.Abbreviation = model.Abbreviation;
            organization.Name = model.Name;
            organization.Description = model.Description;
            this.Context.SaveChanges();

            var organizationViewModel = this.Mapper.Map<OrganizationViewModel>(organization);

            return organizationViewModel;
        }

        public bool CheckUserHasOrganization(string username)
        {
            return this.Context.Organizations.Any(o => o.President.UserName == username);
        }

        public OrganizationViewModel GetOrganizationById(int id)
        {
            var organization = this.Context
                .Organizations
                .FirstOrDefault(o => o.Id == id);

            var organizationViewModel = this.Mapper.Map<OrganizationViewModel>(organization);

            return organizationViewModel;
        }

        public void DeleteOrganization(OrganizationViewModel model)
        {
            var organization = this.Context
                .Organizations
                .FirstOrDefault(d => d.Id == model.Id);

            if (organization != null)
            {
                this.Context.Organizations.Remove(organization);
                this.Context.SaveChanges();
            }
        }
    }
}