namespace Sportify.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Event : IEquatable<Event>
    {
        public Event()
        {
            this.Participants = new List<Participant>();
        }

        public int Id { get; set; }

        public string EventName { get; set; }

        public DateTime Date { get; set; }

        public int OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }

        public int DisciplineId { get; set; }
        public virtual Discipline Discipline { get; set; }

        public int VenueId { get; set; }
        public virtual Venue Venue { get; set; }

        public int NumberOfParticipants { get; set; }

        public virtual ICollection<Participant> Participants { get; set; }

        public bool Equals(Event other)
        {
            return this.Id == other.Id
                && this.EventName == other.EventName 
                && this.Date == other.Date
                && this.OrganizationId == other.OrganizationId 
                && this.DisciplineId == other.DisciplineId
                && this.VenueId == other.VenueId 
                && this.NumberOfParticipants == other.NumberOfParticipants;
        }
    }
}