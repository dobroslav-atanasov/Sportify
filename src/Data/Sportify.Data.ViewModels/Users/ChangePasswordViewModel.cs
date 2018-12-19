namespace Sportify.Data.ViewModels.Users
{
    using System.ComponentModel.DataAnnotations;
    using Constants;

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = Constants.ChangePassword_Display_CurrentPassword)]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(Constants.MaxPasswordLength, ErrorMessage = Constants.PasswordLengthErrorMessage, MinimumLength = Constants.MinPasswordLength)]
        [DataType(DataType.Password)]
        [Display(Name = Constants.ChangePassword_Display_NewPassword)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare(Constants.ChangePassword_Compare, ErrorMessage = Constants.ConfirmPasswordErrorMessage)]
        [Display(Name = Constants.ChangePassword_Display_ConfirmNewPassword)]
        public string ConfirmPassword { get; set; }
    }
}