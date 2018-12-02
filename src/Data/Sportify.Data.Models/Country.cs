namespace Sportify.Data.Models
{
    using System.Collections.Generic;

    public class Country
    {
        public Country()
        {
            this.Towns = new List<Town>();
            this.Users = new List<User>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int Population { get; set; }

        public int LandArea { get; set; }

        public int Density { get; set; }

        public ICollection<Town> Towns { get; set; }

        public ICollection<User> Users { get; set; }
    }
}