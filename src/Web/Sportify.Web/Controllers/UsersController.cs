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
    }
}