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
        public Sport Sport { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}