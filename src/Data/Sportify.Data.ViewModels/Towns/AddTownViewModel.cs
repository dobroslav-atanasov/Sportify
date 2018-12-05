namespace Sportify.Data.ViewModels.Towns
{
    using System.ComponentModel.DataAnnotations;
    using Constants;

    public class AddTownViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [RegularExpression(@"[a-zA-z\s]+", ErrorMessage = Constants.TownInvalidSymbolsErrorMessage)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Country")]
        public int CountryId { get; set; }
    }
}