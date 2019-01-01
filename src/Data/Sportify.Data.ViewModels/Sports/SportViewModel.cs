namespace Sportify.Data.ViewModels.Sports
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Constants;

    public class SportViewModel : IEquatable<SportViewModel>
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [MinLength(ModelConstants.MinSportNameLength, ErrorMessage = ModelConstants.SportNameLengthErrorMessage)]
        [RegularExpression(ModelConstants.AddSport_Regex_Name, ErrorMessage = ModelConstants.SportNameInvalidSymbolsErrorMessage)]
        [Display(Name = ModelConstants.AddSport_Display_Name)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = ModelConstants.AddSport_Display_Description)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = ModelConstants.AddSport_Display_ImageUrl)]
        public string ImageSportUrl { get; set; }

        public bool Equals(SportViewModel other)
        {
            return this.Id == other.Id && this.Name == other.Name && 
                   this.Description == other.Description && this.ImageSportUrl == other.ImageSportUrl;
        }
    }
}