namespace Sportify.Data.ViewModels.Towns
{
    using System.ComponentModel.DataAnnotations;

    using Constants;

    public class AddTownViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [MinLength(ModelConstants.MinTownNameLength, ErrorMessage = ModelConstants.TownNameLengthErrorMessage)]
        [RegularExpression(ModelConstants.AddTown_Regex_Name, ErrorMessage = ModelConstants.TownInvalidSymbolsErrorMessage)]
        [Display(Name = ModelConstants.AddTown_Display_Name)]
        public string Name { get; set; }

        [Required]
        [Display(Name = ModelConstants.AddTown_Display_Country)]
        public int CountryId { get; set; }
    }
}