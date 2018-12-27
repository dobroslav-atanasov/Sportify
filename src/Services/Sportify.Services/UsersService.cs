namespace Sportify.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Data;
    using Data.Models;
    using Data.ViewModels.Users;
    using global::AutoMapper;
    using Interfaces;
    using Microsoft.AspNetCore.Identity;

    public class UsersService : BaseService, IUsersService
    {
        public UsersService(SportifyDbContext context, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
            : base(context, mapper, userManager, signInManager)
        {
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