using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Sportify.Data;
using Sportify.Data.Models;
using Sportify.Data.ViewModels.Organizations;
using Sportify.Services.Interfaces;

namespace Sportify.Services
{
    public class OrganizationsService : BaseService, IOrganizationsService
    {
        public OrganizationsService(SportifyDbContext context, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager) 
            : base(context, mapper, userManager, signInManager)
        {
        }

        public void Create(CreateOrganizationViewModel model, User user)
        {
            var organization = this.Mapper.Map<Organization>(model);
            organization.President = user;

            this.Context.Organizations.Add(organization);
            this.Context.SaveChanges();
        }
    }
}
