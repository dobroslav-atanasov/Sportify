namespace Sportify.Data.ViewModels.Users
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Constants;

    public class UpdateAccountViewModel
    {
        [Display(Name = Constants.CreateAccount_Display_Username)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = Constants.CreateAccount_Display_Email)]
        public string Email { get; set; }
        
        [DataType(DataType.PhoneNumber)]
        [Display(Name = Constants.UpdateAccount_Display_PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = Constants.CreateAccount_Display_BirthDate)]
        public DateTime BirthDate { get; set; }

        [Required] [Display(Name = Constants.CreateAccount_Display_Country)]
        public int CountryId { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = Constants.CreateAccount_Display_FirstName)]
        public string FirstName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = Constants.CreateAccount_Display_LastName)]
        public string LastName { get; set; }
    }
}