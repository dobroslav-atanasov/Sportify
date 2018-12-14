namespace Sportify.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Organization : IEquatable<Organization>
    {
        public Organization()
        {
            this.Events = new List<Event>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string PresidentId { get; set; }
        public virtual User President { get; set; }

        public virtual ICollection<Event> Events { get; set; }

        public bool Equals(Organization other)
        {
            return this.Id == other.Id
                && this.Name == other.Name
                && this.Description == other.Description;
        }
    }
}