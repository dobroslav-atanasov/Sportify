namespace Sportify.Data.Models
{
    using System.Collections.Generic;
    using Common;

    public class Country : BaseModel<int>
    {
        public Country()
        {
            this.Towns = new List<Town>();
            this.Users = new List<User>();
        }

        public string Name { get; set; }

        public string CountryCode { get; set; }

        public int LandArea { get; set; }

        public int Population { get; set; }

        public ICollection<Town> Towns { get; set; }

        public ICollection<User> Users { get; set; }
    }
}