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
    using X.PagedList;

    [Area(Area.Identity)]
    public class UsersController : Microsoft.AspNetCore.Mvc.Controller
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
            this.ViewData[Global.Countries] = this.countriesService.GetAllCountryNames();
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount(CreateAccountViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.ViewData[Global.Countries] = this.countriesService.GetAllCountryNames();
                return this.View(model);
            }

            var country = this.countriesService.GetCountryById(model.CountryId);
            var user = this.mapper.Map<User>(model);
            var result = await this.userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                if (this.userManager.Users.Count() == 1)
                {
                    await this.userManager.AddToRoleAsync(user, Role.Administrator);
                }
                else
                {
                    await this.userManager.AddToRoleAsync(user, Role.User);
                }

                await this.signInManager.SignInAsync(user, false);
            }
            else
            {
                this.ViewData[Global.Error] = Global.UsernameAlreadyExists;
                this.ViewData[Global.Countries] = this.countriesService.GetAllCountryNames();
                return this.View(model);
            }

            return this.RedirectToAction(Actions.HomeIndex, Controllers.Home, new { area = Area.Base });
        }

        public IActionResult SignIn(string returnUrl = null)
        {
            returnUrl = returnUrl ?? this.Url.Content(Global.DefaultUrl);
            var model = new SignInViewModel { ReturnUrl = returnUrl };
            return this.View(model);
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
                this.ViewData[Global.Error] = Global.UsernameOrPasswordAreInvalid;
                this.ViewData[Global.Countries] = this.countriesService.GetAllCountryNames();
                return this.View(model);
            }

            return this.LocalRedirect(model.ReturnUrl);
            //return this.RedirectToAction("Index", "Home", new { area = "" });
        }

        [Authorize]
        public async Task<IActionResult> SignOut()
        {
            await this.signInManager.SignOutAsync();
            return this.RedirectToAction(Actions.HomeIndex, Controllers.Home, new { area = Area.Base });
        }

        [Authorize]
        public IActionResult UpdateAccount()
        {
            var user = this.userManager.FindByNameAsync(this.User.Identity.Name).GetAwaiter().GetResult();
            var model = this.mapper.Map<UpdateAccountViewModel>(user);
            this.ViewData[Global.Countries] = this.countriesService.GetAllCountryNames();
            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateAccount(UpdateAccountViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.ViewData[Global.Countries] = this.countriesService.GetAllCountryNames();
                return this.View(model);
            }

            var user = await this.userManager.FindByNameAsync(this.User.Identity.Name);
            //if (this.usersService.IsUsernameExist(model.Username))
            //{
            //    this.ViewData["Error"] = Constants.UsernameAlreadyExists;
            //    this.ViewData["Countries"] = this.countriesService.GetAllCountryNames();
            //    return this.View(model);
            //}

            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.BirthDate = model.BirthDate;
            user.CountryId = model.CountryId;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            await this.userManager.UpdateAsync(user);

            this.ViewData[Global.Message] = Global.ProfileUpdated;
            this.ViewData[Global.Countries] = this.countriesService.GetAllCountryNames();
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
                this.ViewData[Global.Error] = Global.PasswordWasNotChanged;
                return this.View(model);
            }

            this.ViewData[Global.Message] = Global.PasswordWasChangedSuccessfully;
            return this.View();
        }

        [Authorize(Roles = Role.Administrator)]
        public IActionResult All(int? page)
        {
            var users = this.usersService.GetAllUsers();

            var pageNumber = page ?? 1;
            var usersOnPage = users.ToPagedList(pageNumber, 10);

            this.ViewData[Global.Users] = usersOnPage;

            return this.View();
        }

        [Authorize(Roles = Role.Administrator)]
        public IActionResult ChangeRole(ChangeRoleUserViewModel model)
        {
            var user = this.userManager.Users.FirstOrDefault(u => u.Id == model.UserId);
            var role = this.userManager.GetRolesAsync(user).GetAwaiter().GetResult();

            if (role[0] == Role.User)
            {
                var userRole = this.context.UserRoles.FirstOrDefault(ur => ur.UserId == user.Id);
                this.context.UserRoles.Remove(userRole);

                this.userManager.AddToRoleAsync(user, Role.Editor).GetAwaiter().GetResult();
            }
            else if (role[0] == Role.Editor)
            {
                var userRole = this.context.UserRoles.FirstOrDefault(ur => ur.UserId == user.Id);
                this.context.UserRoles.Remove(userRole);

                this.userManager.AddToRoleAsync(user, Role.User).GetAwaiter().GetResult();
            }

            return this.RedirectToAction(Actions.UsersAll, Controllers.Users, new { area = Area.Identity });
        }
    }
}