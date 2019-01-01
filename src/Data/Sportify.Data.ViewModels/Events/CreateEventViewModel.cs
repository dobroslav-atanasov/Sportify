namespace Sportify.Data.ViewModels.Events
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Constants;

    public class CreateEventViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [RegularExpression(ModelConstants.CreateEvent_Regex_EventName, ErrorMessage = ModelConstants.EventNameInvalidSymbolsErrorMessage)]
        [Display(Name = ModelConstants.CreateEvent_Display_EventName)]
        public string EventName { get; set; }
        
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = ModelConstants.CreateEvent_Display_Date)]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = ModelConstants.CreateEvent_Display_Oraganization)]
        public int OrganizationId { get; set; }

        [Required]
        [Display(Name = ModelConstants.CreateEvent_Display_Discipline)]
        public int DisciplineId { get; set; }

        [Required]
        [Display(Name = ModelConstants.CreateEvent_Display_Venue)]
        public int VenueId { get; set; }

        [Required]
        [Range(ModelConstants.MinNumberOfParticipants, ModelConstants.MaxNumberOfParticipants)]
        [Display(Name = ModelConstants.CreateEvent_Display_NumberOfParticipants)]
        public int NumberOfParticipants { get; set; }
    }
}