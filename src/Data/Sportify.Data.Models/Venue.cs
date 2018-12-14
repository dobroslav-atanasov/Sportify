namespace Sportify.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Venue : IEquatable<Venue>
    {
        public Venue()
        {
            this.Events = new List<Event>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public int Capacity { get; set; }

        public string ImageVenueUrl { get; set; }

        public int TownId { get; set; }
        public virtual Town Town { get; set; }

        public virtual ICollection<Event> Events { get; set; }

        public bool Equals(Venue other)
        {
            return this.Id == other.Id
                && this.Name == other.Name
                && this.Address == other.Address
                && this.Capacity == other.Capacity
                && this.ImageVenueUrl == other.ImageVenueUrl
                && this.TownId == other.TownId;
        }
    }
}