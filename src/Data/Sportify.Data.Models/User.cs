namespace Sportify.Data.Models
{
    using System;
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public string PhotoUrl { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}