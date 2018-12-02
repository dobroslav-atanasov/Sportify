namespace Sportify.Data.Models
{
    using System;

    public class Participant
    {
        public int Id { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public DateTime UserResult { get; set; }
    }
}