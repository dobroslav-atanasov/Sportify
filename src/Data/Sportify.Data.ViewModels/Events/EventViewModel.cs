namespace Sportify.Data.ViewModels.Events
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Constants;

    public class EventViewModel : IEquatable<EventViewModel>
    {
        public int Id { get; set; }

        [Display(Name = ModelConstants.CreateEvent_Display_EventName)]
        public string EventName { get; set; }

        [Display(Name = ModelConstants.CreateEvent_Display_Date)]
        public string Date { get; set; }

        public string Time { get; set; }

        public string Organization { get; set; }

        public string Sport { get; set; }

        public string Discipline { get; set; }

        public string Town { get; set; }

        public string Venue { get; set; }

        public string ImageVenueUrl { get; set; }

        [Display(Name = ModelConstants.CreateEvent_Display_NumberOfParticipants)]
        public int NumberOfParticipants { get; set; }

        public bool Equals(EventViewModel other)
        {
            return this.EventName == other.EventName && this.Date == other.Date && 
                   this.Time == other.Time && this.NumberOfParticipants == other.NumberOfParticipants;
        }
    }
}