namespace Sportify.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
    using Data;
    using Data.Models;
    using Data.ViewModels.Users;
    using global::AutoMapper;
    using Interfaces;
    using Microsoft.AspNetCore.Identity;

    public class UsersService : IUsersService
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly ICountriesService countriesService;
        private readonly IMapper mapper;
        private readonly SportifyDbContext context;

        public UsersService(SignInManager<User> signInManager, UserManager<User> userManager, ICountriesService countriesService, IMapper mapper, SportifyDbContext context)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.countriesService = countriesService;
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<bool> CreateAccountAsync(CreateAccountViewModel model)
        {
            var country = this.countriesService.GetCountryById(model.CountryId);

            var user = this.mapper.Map<User>(model);

            var result = this.userManager.CreateAsync(user, model.Password).GetAwaiter().GetResult();

            if (result.Succeeded)
            {
                if (this.userManager.Users.Count() == 1)
                {
                    await this.userManager.AddToRoleAsync(user, "Administrator");
                }
                else
                {
                    await this.userManager.AddToRoleAsync(user, "User");
                }

                this.signInManager.SignInAsync(user, false).Wait();
            }


            return result.Succeeded;
        }

        public bool SignIn(SignInViewModel model)
        {
            var result = this.signInManager.PasswordSignInAsync(model.Username, model.Password, true, true).GetAwaiter().GetResult();

            return result.Succeeded;
        }

        public void SignOut()
        {
            this.signInManager.SignOutAsync().Wait();
        }

        public IEnumerable<UserAdminViewModel> GetAllUsers()
        {
            var users = this.context
                .Users
                .OrderBy(x => x.UserName)
                .AsQueryable();

            var usersAdminViewModel = this.mapper.Map<IQueryable<User>, IEnumerable<UserAdminViewModel>>(users);

            return usersAdminViewModel;
        }
    }
}