namespace Sportify.Data.ViewModels.Countries
{
    using System.ComponentModel.DataAnnotations;

    public class SearchCountryViewModel
    {
        [Required]
        public int CountryId { get; set; }
    }
}