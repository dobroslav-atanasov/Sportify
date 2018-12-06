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
        public virtual User President { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}