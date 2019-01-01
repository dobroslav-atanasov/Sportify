namespace Sportify.Data.ViewModels.Users
{
    using System.ComponentModel.DataAnnotations;

    using Constants;

    public class SignInViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = ModelConstants.CreateAccount_Display_Username)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = ModelConstants.CreateAccount_Display_Password)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}