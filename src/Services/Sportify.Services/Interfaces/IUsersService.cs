namespace Sportify.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data.ViewModels.Users;

    public interface IUsersService
    {
        Task<bool> CreateAccountAsync(CreateAccountViewModel model);

        bool SignIn(SignInViewModel model);

        void SignOut();

        IEnumerable<UserAdminViewModel> GetAllUsers();

        ProfileUserViewModel GetCurrentUser(string username);

        bool UpdateProfile(ProfileUserViewModel model);

        bool IsUsernameExist(string username);

        bool ChangePassword(string username, ChangePasswordViewModel model);
    }
}