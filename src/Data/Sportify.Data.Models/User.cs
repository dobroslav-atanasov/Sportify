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
            this.Messages = new List<Message>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public string PhotoUrl { get; set; }

        public int CountryId { get; set; }
        public virtual Country Country { get; set; }

        public virtual ICollection<Organization> Organizations { get; set; }

        public virtual ICollection<Participant> Participants { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}