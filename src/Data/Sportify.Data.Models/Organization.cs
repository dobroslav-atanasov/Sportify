namespace Sportify.Data.Models
{
    using System.Collections.Generic;

    public class Organization
    {
        public Organization()
        {
            this.Events = new List<Event>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int PresidentId { get; set; }
        public User President { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}