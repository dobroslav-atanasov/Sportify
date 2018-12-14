namespace Sportify.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
    using Constants;
    using Data;
    using Data.Models;
    using Data.ViewModels.Users;
    using global::AutoMapper;
    using Interfaces;
    using Microsoft.AspNetCore.Identity;

    public class UsersService : BaseService, IUsersService
    {
        private readonly ICountriesService countriesService;

        public UsersService(SportifyDbContext context, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, ICountriesService countriesService)
            : base(context, mapper, userManager, signInManager)
        {
            this.countriesService = countriesService;
        }

        public IEnumerable<UserAdminViewModel> GetAllUsers()
        {
            var users = this.Context
                .Users
                .OrderBy(x => x.UserName)
                .AsQueryable();

            var usersAdminViewModel = this.Mapper.Map<IQueryable<User>, IEnumerable<UserAdminViewModel>>(users);

            return usersAdminViewModel;
        }

        public bool IsUsernameExist(string username)
        {
            return this.Context.Users.Any(u => u.UserName == username);
        }
    }
}