namespace Sportify.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Town : IEquatable<Town>
    {
        public Town()
        {
            this.Venues = new List<Venue>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int CountryId { get; set; }
        public virtual Country Country { get; set; }

        public virtual ICollection<Venue> Venues { get; set; }

        public bool Equals(Town other)
        {
            return this.Id == other.Id
                && this.Name == other.Name
                && this.CountryId == other.CountryId;
        }
    }
}