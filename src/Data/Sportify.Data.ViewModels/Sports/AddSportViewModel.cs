namespace Sportify.Data.ViewModels.Sports
{
    using System.ComponentModel.DataAnnotations;
    using Constants;

    public class AddSportViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [MinLength(Constants.MinSportNameLength, ErrorMessage = Constants.SportNameLengthErrorMessage)]
        [RegularExpression(Constants.AddSport_Regex_Name, ErrorMessage = Constants.SportNameInvalidSymbolsErrorMessage)]
        [Display(Name = Constants.AddSport_Display_Name)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = Constants.AddSport_Display_Description)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = Constants.AddSport_Display_ImageUrl)]
        public string ImageSportUrl { get; set; }
    }
}