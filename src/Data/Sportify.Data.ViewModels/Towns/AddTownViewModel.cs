namespace Sportify.Data.ViewModels.Towns
{
    using System.ComponentModel.DataAnnotations;
    using Constants;

    public class AddTownViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [MinLength(Constants.MinTownNameLength, ErrorMessage = Constants.TownNameLengthErrorMessage)]
        [RegularExpression(Constants.AddTown_Regex_Name, ErrorMessage = Constants.TownInvalidSymbolsErrorMessage)]
        [Display(Name = Constants.AddTown_Display_Name)]
        public string Name { get; set; }

        [Required]
        [Display(Name = Constants.AddTown_Display_Country)]
        public int CountryId { get; set; }
    }
}