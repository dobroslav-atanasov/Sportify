﻿namespace Sportify.Data.Models
{
    using System.Collections.Generic;

    public class Venue
    {
        public Venue()
        {
            this.Events = new List<Event>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int Capacity { get; set; }

        public string ImageVenueUrl { get; set; }

        public int TownId { get; set; }
        public Town Town { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}