namespace Sportify.Data.ViewModels.Venues
{
    using System.ComponentModel.DataAnnotations;

    public class AddVenueViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [Range(1, 100000)]
        [Display(Name = "Capacity")]
        public int Capacity { get; set; }

        [Required]
        [DataType(DataType.Url)]
        [Display(Name = "Image Venue")]
        public string ImageVenueUrl { get; set; }

        [Required] [Display(Name = "Town")]
        public int TownId { get; set; }
    }
}