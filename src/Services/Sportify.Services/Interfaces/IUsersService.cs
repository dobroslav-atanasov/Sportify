namespace Sportify.Services.Interfaces
{
    using System.Threading.Tasks;
    using Data.ViewModels.Users;

    public interface IUsersService
    {
        Task<bool> CreateAccountAsync(CreateAccountViewModel model);

        bool Login(string username, string password, bool rememberMe);

        void Logout();
    }
}