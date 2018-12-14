namespace Sportify.Data.ViewModels.Events
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Constants;

    public class CreateEventViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [RegularExpression("[a-zA-z0-9-.*/_\\s]+", ErrorMessage = Constants.EventNameInvalidSymbolsErrorMessage)]
        [Display(Name = "Event Name")]
        public string EventName { get; set; }
        
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Date and Time of Events")]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Organization")]
        public int OrganizationId { get; set; }

        [Required]
        [Display(Name = "Discipline")]
        public int DisciplineId { get; set; }

        [Required]
        [Display(Name = "Venue")]
        public int VenueId { get; set; }

        [Required]
        [Range(Constants.MinNumberOfParticipants, Constants.MaxNumberOfParticipants)]
        [Display(Name = "Number of Participants")]
        public int NumberOfParticipants { get; set; }
    }
}