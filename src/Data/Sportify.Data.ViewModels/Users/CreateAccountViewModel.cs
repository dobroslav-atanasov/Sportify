namespace Sportify.Data.ViewModels.Users
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Constants;

    public class CreateAccountViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [MinLength(ModelConstants.MinUsernameLength, ErrorMessage = ModelConstants.UsernameLengthErrorMessage)]
        [RegularExpression(ModelConstants.CreateAccount_Regex_Username, ErrorMessage = ModelConstants.UsernameInvalidSymbolsErrorMessage)]
        [Display(Name = ModelConstants.CreateAccount_Display_Username)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = ModelConstants.CreateAccount_Display_Email)]
        public string Email { get; set; }

        [Required]
        [StringLength(ModelConstants.MaxPasswordLength, ErrorMessage = ModelConstants.PasswordLengthErrorMessage, MinimumLength = ModelConstants.MinPasswordLength)]
        [DataType(DataType.Password)]
        [Display(Name = ModelConstants.CreateAccount_Display_Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(ModelConstants.CreateAccount_Compare, ErrorMessage = ModelConstants.ConfirmPasswordErrorMessage)]
        [Display(Name = ModelConstants.CreateAccount_Display_ConfirmPassword)]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = ModelConstants.CreateAccount_Display_FirstName)]
        public string FirstName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = ModelConstants.CreateAccount_Display_LastName)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = ModelConstants.CreateAccount_Display_BirthDate)]
        public DateTime BirthDate { get; set; }

        [Required]
        [Display(Name = ModelConstants.CreateAccount_Display_Country)]
        public int CountryId { get; set; }
    }
}