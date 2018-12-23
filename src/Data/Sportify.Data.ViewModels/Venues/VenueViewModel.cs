namespace Sportify.Data.ViewModels.Venues
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Constants;

    public class VenueViewModel : IEquatable<VenueViewModel>
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [MinLength(ModelConstants.MinVenueNameLength, ErrorMessage = ModelConstants.VenueNameLengthErrorMessage)]
        [RegularExpression(ModelConstants.AddVenue_Regex_Name, ErrorMessage = ModelConstants.VenueNameInvalidSymbolsErrorMessage)]
        [Display(Name = ModelConstants.AddVenue_Display_Name)]
        public string Name { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = ModelConstants.AddVenue_Display_Address)]
        public string Address { get; set; }

        [Required]
        [Range(ModelConstants.MinVenueCapacity, ModelConstants.MaxVenueCapacity)]
        [Display(Name = ModelConstants.AddVenue_Display_Capacity)]
        public int Capacity { get; set; }

        [Required]
        [DataType(DataType.Url)]
        [Display(Name = ModelConstants.AddVenue_Display_ImageUrl)]
        public string ImageVenueUrl { get; set; }

        [Required]
        [Display(Name = ModelConstants.AddVenue_Display_Town)]
        public int TownId { get; set; }

        public string Town { get; set; }

        public string Country { get; set; }

        public bool Equals(VenueViewModel other)
        {
            return this.Name == other.Name && this.Capacity == other.Capacity &&
                   this.ImageVenueUrl == other.ImageVenueUrl && this.TownId == other.TownId;
        }
    }
}