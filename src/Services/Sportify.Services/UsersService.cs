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

        public async Task<bool> CreateAccountAsync(CreateAccountViewModel model)
        {
            var country = this.countriesService.GetCountryById(model.CountryId);

            var user = this.Mapper.Map<User>(model);

            var result = this.UserManager.CreateAsync(user, model.Password).GetAwaiter().GetResult();

            if (result.Succeeded)
            {
                if (this.UserManager.Users.Count() == 1)
                {
                    await this.UserManager.AddToRoleAsync(user, "Administrator");
                }
                else
                {
                    await this.UserManager.AddToRoleAsync(user, "User");
                }

                this.SignInManager.SignInAsync(user, false).Wait();
            }


            return result.Succeeded;
        }

        public bool SignIn(SignInViewModel model)
        {
            var result = this.SignInManager.PasswordSignInAsync(model.Username, model.Password, true, true).GetAwaiter().GetResult();

            return result.Succeeded;
        }

        public void SignOut()
        {
            this.SignInManager.SignOutAsync().Wait();
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

        public ProfileUserViewModel GetCurrentUser(string username)
        {
            var user = this.UserManager.FindByNameAsync(username).GetAwaiter().GetResult();
            var model = this.Mapper.Map<ProfileUserViewModel>(user);

            return model;
        }

        public bool UpdateProfile(string username, ProfileUserViewModel model)
        {
            var user = this.UserManager.FindByNameAsync(username).GetAwaiter().GetResult();

            if (this.IsUsernameExist(model.Username))
            {
                return false;
            }

            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.BirthDate = model.BirthDate;
            user.CountryId = model.CountryId;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            var isUpdatedUser = this.UserManager.UpdateAsync(user).GetAwaiter().GetResult();
            if (!isUpdatedUser.Succeeded)
            {
                return false;
            }

            return true;
        }

        public bool IsUsernameExist(string username)
        {
            return this.Context.Users.Any(u => u.UserName == username);
        }

        public bool ChangePassword(string username, ChangePasswordViewModel model)
        {
            var user = this.UserManager.FindByNameAsync(username).GetAwaiter().GetResult();
            var result = this.UserManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword).GetAwaiter().GetResult();

            if (!result.Succeeded)
            {
                return false;
            }

            return true;
        }

        public void ChangeRole(UserIdViewModel model)
        {
            var user = this.UserManager.Users.FirstOrDefault(u => u.Id == model.UserId);

            var role = this.UserManager.GetRolesAsync(user).GetAwaiter().GetResult();

            if (role[0] == Constants.UserRole)
            {
                var userRole = this.Context.UserRoles.FirstOrDefault(ur => ur.UserId == user.Id);
                this.Context.UserRoles.Remove(userRole);

                this.UserManager.AddToRoleAsync(user, Constants.EditorRole).GetAwaiter().GetResult();
            }
            else if (role[0] == Constants.EditorRole)
            {
                var userRole = this.Context.UserRoles.FirstOrDefault(ur => ur.UserId == user.Id);
                this.Context.UserRoles.Remove(userRole);

                this.UserManager.AddToRoleAsync(user, Constants.UserRole).GetAwaiter().GetResult();
            }
        }
    }
}