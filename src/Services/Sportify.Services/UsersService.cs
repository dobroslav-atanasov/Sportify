namespace Sportify.Services
{
    using System.Threading.Tasks;
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

        public UsersService(SignInManager<User> signInManager, UserManager<User> userManager, ICountriesService countriesService, IMapper mapper)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.countriesService = countriesService;
            this.mapper = mapper;
        }

        public async Task<bool> CreateAccountAsync(CreateAccountViewModel model)
        {
            var country = this.countriesService.GetCountryById(model.CountryId);

            var user = this.mapper.Map<User>(model);

            var result = this.userManager.CreateAsync(user, model.Password).Result;

            if (result.Succeeded)
            {
                await this.userManager.AddToRoleAsync(user, "User");

                this.signInManager.SignInAsync(user, false).Wait();
            }

            return result.Succeeded;
        }

        public bool Login(string username, string password, bool rememberMe)
        {
            throw new System.NotImplementedException();
        }

        public void Logout()
        {
            throw new System.NotImplementedException();
        }
    }
}