namespace Sportify.Web.Models.Users
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Constants;

    public class RegisterViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [MinLength(3, ErrorMessage = Constants.UsernameLengthErrorMessage)]
        [RegularExpression("[a-zA-z0-9-.*/_]+", ErrorMessage = Constants.UsernameInvalidSymbolsErrorMessage)]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = Constants.PasswordLengthErrorMessage, MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = Constants.ConfirmPasswordErrorMessage)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }

        [Required] [Display(Name = "Country")] public int CountryId { get; set; }
    }
}