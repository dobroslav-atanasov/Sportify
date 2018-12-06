namespace Sportify.Data.Models
{
    using System;

    public class Participant
    {
        public int Id { get; set; }

        public int EventId { get; set; }
        public virtual Event Event { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public DateTime UserResult { get; set; }
    }
}