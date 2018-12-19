namespace Sportify.Data.ViewModels.Users
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Constants;

    public class CreateAccountViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [MinLength(Constants.MinUsernameLength, ErrorMessage = Constants.UsernameLengthErrorMessage)]
        [RegularExpression(Constants.CreateAccount_Regex_Username, ErrorMessage = Constants.UsernameInvalidSymbolsErrorMessage)]
        [Display(Name = Constants.CreateAccount_Display_Username)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = Constants.CreateAccount_Display_Email)]
        public string Email { get; set; }

        [Required]
        [StringLength(Constants.MaxPasswordLength, ErrorMessage = Constants.PasswordLengthErrorMessage, MinimumLength = Constants.MinPasswordLength)]
        [DataType(DataType.Password)]
        [Display(Name = Constants.CreateAccount_Display_Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(Constants.CreateAccount_Compare, ErrorMessage = Constants.ConfirmPasswordErrorMessage)]
        [Display(Name = Constants.CreateAccount_Display_ConfirmPassword)]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = Constants.CreateAccount_Display_FirstName)]
        public string FirstName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = Constants.CreateAccount_Display_LastName)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = Constants.CreateAccount_Display_BirthDate)]
        public DateTime BirthDate { get; set; }

        [Required]
        [Display(Name = Constants.CreateAccount_Display_Country)]
        public int CountryId { get; set; }
    }
}