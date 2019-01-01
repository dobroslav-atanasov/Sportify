namespace Sportify.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Sport : IEquatable<Sport>
    {
        public Sport()
        {
            this.Disciplines = new List<Discipline>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageSportUrl { get; set; }

        public virtual ICollection<Discipline> Disciplines { get; set; }

        public bool Equals(Sport other)
        {
            return this.Id == other.Id && this.Name == other.Name && 
                   this.Description == other.Description && this.ImageSportUrl == other.ImageSportUrl;
        }
    }
}