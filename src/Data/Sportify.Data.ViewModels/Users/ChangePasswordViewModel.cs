namespace Sportify.Data.ViewModels.Users
{
    using System.ComponentModel.DataAnnotations;
    using Constants;

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(Constants.MaxPasswordLength, ErrorMessage = Constants.PasswordLengthErrorMessage, MinimumLength = Constants.MaxPasswordLength)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = Constants.ConfirmPasswordErrorMessage)]
        [Display(Name = "Confirm new password")]
        public string ConfirmPassword { get; set; }
    }
}