namespace Sportify.Services.Interfaces
{
    using System.Threading.Tasks;
    using Data.ViewModels.Users;

    public interface IUsersService
    {
        Task<bool> CreateAccountAsync(CreateAccountViewModel model);

        bool SignIn(SignInViewModel model);

        void SignOut();
    }
}