namespace Sportify.Data.ViewModels.Messages
{
    using System.ComponentModel.DataAnnotations;
    using Constants;

    public class SendMessageViewModel
    {
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [MinLength(Constants.MinUsernameLength, ErrorMessage = Constants.MessageFullNameLength)]
        [RegularExpression("[a-zA-z0-9-.*/_\\s]+", ErrorMessage = Constants.MessageFullNameContainsInvalidSymbols)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Subject")]
        public string Subject { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Content")]
        public string Content { get; set; }
    }
}