﻿namespace Sportify.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Discipline : IEquatable<Discipline>
    {
        public Discipline()
        {
            this.Events = new List<Event>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int SportId { get; set; }
        public virtual Sport Sport { get; set; }

        public virtual ICollection<Event> Events { get; set; }

        public bool Equals(Discipline other)
        {
            return this.Id == other.Id && this.Name == other.Name && 
                   this.Description == other.Description && this.SportId == other.SportId;
        }
    }
}