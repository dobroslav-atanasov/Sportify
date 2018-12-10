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
        [StringLength(10, ErrorMessage = Constants.PasswordLengthErrorMessage, MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = Constants.ConfirmPasswordErrorMessage)]
        [Display(Name = "Confirm new password")]
        public string ConfirmPassword { get; set; }
    }
}