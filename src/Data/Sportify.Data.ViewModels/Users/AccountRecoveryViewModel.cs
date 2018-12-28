namespace Sportify.Data.ViewModels.Users
{
    using System.ComponentModel.DataAnnotations;

    public class AccountRecoveryViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}