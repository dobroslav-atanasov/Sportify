namespace Sportify.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Event
    {
        public Event()
        {
            this.Participants = new List<Participant>();
        }

        public int Id { get; set; }

        public string EventName { get; set; }

        public DateTime Date { get; set; }

        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }

        public int DisciplineId { get; set; }
        public Discipline Discipline { get; set; }

        public int VenueId { get; set; }
        public Venue Venue { get; set; }

        public int NumberOfParticipants { get; set; }

        public ICollection<Participant> Participants { get; set; }
    }
}