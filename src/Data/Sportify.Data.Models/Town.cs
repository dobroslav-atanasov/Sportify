namespace Sportify.Data.Models
{
    using System.Collections.Generic;

    public class Town
    {
        public Town()
        {
            this.Venues = new List<Venue>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }

        public ICollection<Venue> Venues { get; set; }
    }
}