﻿namespace Sportify.Web.Areas.Identity.Controllers
{
    using System.Linq;
    using System.Net.Mail;
    using System.Threading.Tasks;

    using Constants;
    using Data;
    using Data.Models;
    using Data.ViewModels.Users;
    using global::AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services.Interfaces;
    using X.PagedList;

    [Area(AreaConstants.Identity)]
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
            this.ViewData[GlobalConstants.Countries] = this.countriesService.GetAllCountryNames();
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount(CreateAccountViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.ViewData[GlobalConstants.Countries] = this.countriesService.GetAllCountryNames();
                return this.View(model);
            }

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
                this.ViewData[GlobalConstants.Error] = GlobalConstants.UsernameAlreadyExists;
                this.ViewData[GlobalConstants.Countries] = this.countriesService.GetAllCountryNames();
                return this.View(model);
            }

            return this.RedirectToAction("Index", "Home", new { area = AreaConstants.Base });
        }

        public IActionResult SignIn(string returnUrl = null)
        {
            returnUrl = returnUrl ?? this.Url.Content(GlobalConstants.DefaultUrl);
            var model = new SignInViewModel { ReturnUrl = returnUrl };
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var signInResult = await this.signInManager.PasswordSignInAsync(model.Username, model.Password, true, true);

            if (!signInResult.Succeeded)
            {
                this.ViewData[GlobalConstants.Error] = GlobalConstants.UsernameOrPasswordAreInvalid;
                this.ViewData[GlobalConstants.Countries] = this.countriesService.GetAllCountryNames();
                return this.View(model);
            }

            return this.LocalRedirect(model.ReturnUrl);
        }

        [Authorize]
        public async Task<IActionResult> SignOut()
        {
            await this.signInManager.SignOutAsync();
            return this.RedirectToAction("Index", "Home", new { area = AreaConstants.Base });
        }

        [Authorize]
        public IActionResult UpdateAccount()
        {
            var user = this.userManager.FindByNameAsync(this.User.Identity.Name).GetAwaiter().GetResult();
            var model = this.mapper.Map<UpdateAccountViewModel>(user);
            this.ViewData[GlobalConstants.Countries] = this.countriesService.GetAllCountryNames();
            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateAccount(UpdateAccountViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.ViewData[GlobalConstants.Countries] = this.countriesService.GetAllCountryNames();
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

            this.ViewData[GlobalConstants.Message] = GlobalConstants.ProfileUpdated;
            this.ViewData[GlobalConstants.Countries] = this.countriesService.GetAllCountryNames();
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
                this.ViewData[GlobalConstants.Error] = GlobalConstants.PasswordWasNotChanged;
                return this.View(model);
            }

            this.ViewData[GlobalConstants.Message] = GlobalConstants.PasswordWasChangedSuccessfully;
            return this.View();
        }

        [Authorize(Roles = Role.Administrator)]
        public IActionResult All(int? page)
        {
            var users = this.usersService.GetAllUsers();

            var pageNumber = page ?? 1;
            var usersOnPage = users.ToPagedList(pageNumber, 10);

            this.ViewData[GlobalConstants.Users] = usersOnPage;

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

            return this.RedirectToAction("All", "Users", new { area = AreaConstants.Identity });
        }

        public IActionResult AccountRecovery()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult AccountRecovery(AccountRecoveryViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }



            //var user = this.userManager.FindByEmailAsync(model.Email).GetAwaiter().GetResult();
            //this.userManager.RemovePasswordAsync(user).GetAwaiter().GetResult();
            //this.userManager.AddPasswordAsync(user, "AA1234").GetAwaiter().GetResult();

            SmtpClient smtpClient = new SmtpClient("smtp.abv.bg", 465);

            smtpClient.Credentials = new System.Net.NetworkCredential("sportify_da@abv.bg", "da123456");
            smtpClient.UseDefaultCredentials = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            MailMessage mail = new MailMessage();

            //Setting From , To and CC
            mail.From = new MailAddress("sportify_da@abv.bg", "MyWeb Site");
            mail.To.Add(new MailAddress("dobroslav_atanasov@abv.bg"));
            mail.Body = "Test";

            smtpClient.Send(mail);

            //SmtpClient client = new SmtpClient();
            //client.Port = 995;
            //client.Host = "pop3.abv.bg";
            //client.EnableSsl = true;
            //client.Timeout = 10000;
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.UseDefaultCredentials = false;
            //client.Credentials = new System.Net.NetworkCredential("sportify_da@abv.bg", "da123456");

            //MailMessage mm = new MailMessage("sportify_da@abv.bg", "dobroslav_atanasov@abv.bg", "Test", "New test");
            //mm.BodyEncoding = UTF8Encoding.UTF8;
            //mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            //client.Send(mm);

            this.ViewData[GlobalConstants.Message] = GlobalConstants.RecoveryEmail;
            return this.View();
        }
    }
}