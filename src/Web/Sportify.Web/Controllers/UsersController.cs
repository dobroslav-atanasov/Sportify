namespace Sportify.Web.Controllers
{
    using Constants;
    using Data.ViewModels.Users;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.Interfaces;

    public class UsersController : Controller
    {
        private readonly ICountriesService countriesService;
        private readonly IUsersService usersService;

        public UsersController(ICountriesService countriesService, IUsersService usersService)
        {
            this.countriesService = countriesService;
            this.usersService = usersService;
        }

        public IActionResult CreateAccount()
        {
            this.ViewData["Countries"] = this.countriesService.GetAllCountryNames();
            return this.View();
        }

        [HttpPost]
        public IActionResult CreateAccount(CreateAccountViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.ViewData["Countries"] = this.countriesService.GetAllCountryNames();
                return this.View(model);
            }

            var isRegister = this.usersService.CreateAccountAsync(model).GetAwaiter().GetResult();

            if (!isRegister)
            {
                this.ViewData["Error"] = Constants.UsernameAlreadyExists;
                this.ViewData["Countries"] = this.countriesService.GetAllCountryNames();
                return this.View(model);
            }

            return this.RedirectToAction("Index", "Home");
        }

        public IActionResult SignIn()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult SignIn(SignInViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var isLogin = this.usersService.SignIn(model);

            if (!isLogin)
            {
                this.ViewData["Error"] = Constants.UsernameOrPasswordAreInvalid;
                this.ViewData["Countries"] = this.countriesService.GetAllCountryNames();
                return this.View(model);
            }

            return this.RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult SignOut()
        {
            this.usersService.SignOut();
            return this.RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult Profile()
        {
            var currentUsername = this.User.Identity.Name;
            var model = this.usersService.GetCurrentUser(currentUsername);
            this.ViewData["Countries"] = this.countriesService.GetAllCountryNames();
            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Profile(ProfileUserViewModel model)
        {
            var currentUsername = this.User.Identity.Name;
            if (!this.ModelState.IsValid)
            {
                this.ViewData["Countries"] = this.countriesService.GetAllCountryNames();
                return this.View(model);
            }

            var isUpdated = this.usersService.UpdateProfile(currentUsername, model);
            if (isUpdated)
            {
                this.ViewData["Message"] = Constants.ProfileUpdated;
                this.ViewData["Countries"] = this.countriesService.GetAllCountryNames();
                return this.View(model);
            }
            else
            {
                this.ViewData["Error"] = Constants.UsernameAlreadyExists;
                this.ViewData["Countries"] = this.countriesService.GetAllCountryNames();
                return this.View(model);
            }
        }

        [Authorize]
        public IActionResult ChangePassword()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var currentUsername = this.User.Identity.Name;
            var isChangePassword = this.usersService.ChangePassword(currentUsername, model);
            if (isChangePassword)
            {
                this.ViewData["Message"] = Constants.PasswordWasChangedSuccessfully;
                return this.View();
            }
            else
            {
                this.ViewData["Error"] = Constants.PasswordWasNotChanged;
                return this.View(model);
            }
        }
    }
}