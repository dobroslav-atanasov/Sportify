namespace Sportify.Data.Models
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        public User()
        {
            this.Organizations = new List<Organization>();
            this.Participants = new List<Participant>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public string PhotoUrl { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }

        public ICollection<Organization> Organizations { get; set; }

        public ICollection<Participant> Participants { get; set; }
    }
}