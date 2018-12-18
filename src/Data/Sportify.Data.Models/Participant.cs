namespace Sportify.Data.Models
{
    using System;

    public class Participant : IEquatable<Participant>
    {
        public int Id { get; set; }

        public int EventId { get; set; }
        public virtual Event Event { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        public DateTime UserResult { get; set; }

        public bool Equals(Participant other)
        {
            return this.EventId == other.EventId && this.UserId == other.UserId;
        }
    }
}