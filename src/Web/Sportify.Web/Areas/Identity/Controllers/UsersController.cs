namespace Sportify.Web.Areas.Identity.Controllers
{
    using Constants;
    using Data.ViewModels.Users;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.Interfaces;
    using System.Threading.Tasks;
    using global::AutoMapper;
    using Sportify.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using System.Linq;
    using Sportify.Data;

    [Area("Identity")]
    public class UsersController : Controller
    {
        private readonly SportifyDbContext context;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ICountriesService countriesService;
        private readonly IUsersService usersService;
        private readonly IMapper mapper;

        public UsersController(SportifyDbContext context, UserManager<User> userManager, SignInManager<User> signInManager, ICountriesService countriesService, IUsersService usersService, IMapper mapper)
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.countriesService = countriesService;
            this.usersService = usersService;
            this.mapper = mapper;
        }

        public IActionResult CreateAccount()
        {
            this.ViewData["Countries"] = this.countriesService.GetAllCountryNames();
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount(CreateAccountViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.ViewData["Countries"] = this.countriesService.GetAllCountryNames();
                return this.View(model);
            }

            var country = this.countriesService.GetCountryById(model.CountryId);
            var user = this.mapper.Map<User>(model);
            var result = await this.userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                if (this.userManager.Users.Count() == 1)
                {
                    await this.userManager.AddToRoleAsync(user, Constants.AdministratorRole);
                }
                else
                {
                    await this.userManager.AddToRoleAsync(user, Constants.UserRole);
                }

                await this.signInManager.SignInAsync(user, false);
            }
            else
            {
                this.ViewData["Error"] = Constants.UsernameAlreadyExists;
                this.ViewData["Countries"] = this.countriesService.GetAllCountryNames();
                return this.View(model);
            }

            return this.RedirectToAction("Index", "Home", new { area = "" });
        }

        public IActionResult SignIn()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            // TODO:  Continue with same page!!!

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var signInResult = await this.signInManager.PasswordSignInAsync(model.Username, model.Password, true, true);

            if (!signInResult.Succeeded)
            {
                this.ViewData["Error"] = Constants.UsernameOrPasswordAreInvalid;
                this.ViewData["Countries"] = this.countriesService.GetAllCountryNames();
                return this.View(model);
            }

            return this.RedirectToAction("Index", "Home", new { area = "" });
        }

        [Authorize]
        public async Task<IActionResult> SignOut()
        {
            await this.signInManager.SignOutAsync();
            return this.RedirectToAction("Index", "Home", new { area = "" });
        }

        [Authorize]
        public IActionResult UpdateProfile()
        {
            var user = this.userManager.FindByNameAsync(this.User.Identity.Name).GetAwaiter().GetResult();
            var model = this.mapper.Map<ProfileUserViewModel>(user);
            this.ViewData["Countries"] = this.countriesService.GetAllCountryNames();
            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateProfile(ProfileUserViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.ViewData["Countries"] = this.countriesService.GetAllCountryNames();
                return this.View(model);
            }

            var user = await this.userManager.FindByNameAsync(this.User.Identity.Name);
            if (this.usersService.IsUsernameExist(model.Username))
            {
                this.ViewData["Error"] = Constants.UsernameAlreadyExists;
                this.ViewData["Countries"] = this.countriesService.GetAllCountryNames();
                return this.View(model);
            }

            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.BirthDate = model.BirthDate;
            user.CountryId = model.CountryId;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            await this.userManager.UpdateAsync(user);

            this.ViewData["Message"] = Constants.ProfileUpdated;
            this.ViewData["Countries"] = this.countriesService.GetAllCountryNames();
            return this.View(model);
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

            var user = this.userManager.FindByNameAsync(this.User.Identity.Name).GetAwaiter().GetResult();
            var isChangedPassword = this.userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword).GetAwaiter().GetResult();

            if (!isChangedPassword.Succeeded)
            {
                this.ViewData["Error"] = Constants.PasswordWasNotChanged;
                return this.View(model);
            }

            this.ViewData["Message"] = Constants.PasswordWasChangedSuccessfully;
            return this.View();
        }

        [Authorize(Roles = Constants.AdministratorRole)]
        public IActionResult ChangeRole(UserIdViewModel model)
        {
            var user = this.userManager.Users.FirstOrDefault(u => u.Id == model.UserId);
            var role = this.userManager.GetRolesAsync(user).GetAwaiter().GetResult();

            if (role[0] == Constants.UserRole)
            {
                var userRole = this.context.UserRoles.FirstOrDefault(ur => ur.UserId == user.Id);
                this.context.UserRoles.Remove(userRole);

                this.userManager.AddToRoleAsync(user, Constants.EditorRole).GetAwaiter().GetResult();
            }
            else if (role[0] == Constants.EditorRole)
            {
                var userRole = this.context.UserRoles.FirstOrDefault(ur => ur.UserId == user.Id);
                this.context.UserRoles.Remove(userRole);

                this.userManager.AddToRoleAsync(user, Constants.UserRole).GetAwaiter().GetResult();
            }

            return this.RedirectToAction("AllMessages", "MessagesAdmin", new { area = Constants.AdministratorRole });
        }
    }
}