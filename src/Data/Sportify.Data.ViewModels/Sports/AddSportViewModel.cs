namespace Sportify.Data.ViewModels.Sports
{
    using System.ComponentModel.DataAnnotations;
    using Constants;

    public class AddSportViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [MinLength(3, ErrorMessage = Constants.SportNameLengthErrorMessage)]
        [RegularExpression("[a-zA-z0-9-.*/_]+", ErrorMessage = Constants.NameInvalidSymbolsErrorMessage)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Image Url")]
        public string ImageSportUrl { get; set; }
    }
}