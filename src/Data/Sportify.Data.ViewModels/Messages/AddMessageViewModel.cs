namespace Sportify.Data.ViewModels.Messages
{
    using System.ComponentModel.DataAnnotations;
    using Constants;

    public class AddMessageViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [MinLength(Constants.MinUsernameLength, ErrorMessage = Constants.UsernameLengthErrorMessage)]
        [RegularExpression("[a-zA-z0-9-.*/_\\s]+", ErrorMessage = Constants.UsernameInvalidSymbolsErrorMessage)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Content")]
        public string Content { get; set; }
    }
}