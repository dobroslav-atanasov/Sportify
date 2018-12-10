namespace Sportify.Data.ViewModels.Messages
{
    using System.ComponentModel.DataAnnotations;
    using Constants;

    public class AddMessageViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [MinLength(3, ErrorMessage = Constants.UsernameLengthErrorMessage)]
        [RegularExpression("[a-zA-z0-9-.*/_]+", ErrorMessage = Constants.UsernameInvalidSymbolsErrorMessage)]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Content")]
        public string Content { get; set; }
    }
}