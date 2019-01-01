namespace Sportify.Data.ViewModels.Messages
{
    using System.ComponentModel.DataAnnotations;

    using Constants;

    public class SendMessageViewModel
    {
        [Display(Name = ModelConstants.SendMessage_Display_Username)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [MinLength(ModelConstants.MinUsernameLength, ErrorMessage = ModelConstants.MessageFullNameLength)]
        [RegularExpression(ModelConstants.SendMessage_Regex_FullName, ErrorMessage = ModelConstants.MessageFullNameContainsInvalidSymbols)]
        [Display(Name = ModelConstants.SendMessage_Display_FullName)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = ModelConstants.SendMessage_Display_Email)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = ModelConstants.SendMessage_Display_Subject)]
        public string Subject { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = ModelConstants.SendMessage_Display_Content)]
        public string Content { get; set; }
    }
}