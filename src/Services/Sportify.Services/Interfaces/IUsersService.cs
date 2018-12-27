namespace Sportify.Services.Interfaces
{
    using System.Collections.Generic;

    using Data.ViewModels.Users;

    public interface IUsersService
    {
        IEnumerable<UserAdminViewModel> GetAllUsers();

        bool IsUsernameExist(string username);
    }
}