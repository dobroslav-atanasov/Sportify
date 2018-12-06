namespace Sportify.Data.Models
{
    using System.Collections.Generic;

    public class Discipline
    {
        public Discipline()
        {
            this.Events = new List<Event>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int SportId { get; set; }
        public virtual Sport Sport { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}