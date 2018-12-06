namespace Sportify.Data.Models
{
    using System.Collections.Generic;

    public class Sport
    {
        public Sport()
        {
            this.Disciplines = new List<Discipline>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageSportUrl { get; set; }

        public virtual ICollection<Discipline> Disciplines { get; set; }
    }
}