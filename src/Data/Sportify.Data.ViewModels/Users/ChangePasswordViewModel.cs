namespace Sportify.Data.ViewModels.Users
{
    using System.ComponentModel.DataAnnotations;
    using Constants;

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = ModelConstants.ChangePassword_Display_CurrentPassword)]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(ModelConstants.MaxPasswordLength, ErrorMessage = ModelConstants.PasswordLengthErrorMessage, MinimumLength = ModelConstants.MinPasswordLength)]
        [DataType(DataType.Password)]
        [Display(Name = ModelConstants.ChangePassword_Display_NewPassword)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare(ModelConstants.ChangePassword_Compare, ErrorMessage = ModelConstants.ConfirmPasswordErrorMessage)]
        [Display(Name = ModelConstants.ChangePassword_Display_ConfirmNewPassword)]
        public string ConfirmPassword { get; set; }
    }
}