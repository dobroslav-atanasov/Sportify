namespace Sportify.Data.ViewModels.Events
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Constants;

    public class CreateEventViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [RegularExpression(Constants.CreateEvent_Regex_EventName, ErrorMessage = Constants.EventNameInvalidSymbolsErrorMessage)]
        [Display(Name = Constants.CreateEvent_Display_EventName)]
        public string EventName { get; set; }
        
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = Constants.CreateEvent_Display_Date)]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = Constants.CreateEvent_Display_Oraganization)]
        public int OrganizationId { get; set; }

        [Required]
        [Display(Name = Constants.CreateEvent_Display_Discipline)]
        public int DisciplineId { get; set; }

        [Required]
        [Display(Name = Constants.CreateEvent_Display_Venue)]
        public int VenueId { get; set; }

        [Required]
        [Range(Constants.MinNumberOfParticipants, Constants.MaxNumberOfParticipants)]
        [Display(Name = Constants.CreateEvent_Display_NumberOfParticipants)]
        public int NumberOfParticipants { get; set; }
    }
}