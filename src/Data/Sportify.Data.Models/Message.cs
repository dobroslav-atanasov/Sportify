namespace Sportify.Data.Models
{
    using System;

    public class Message : IEquatable<Message>
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        public DateTime PublishedOn { get; set; } = DateTime.UtcNow;

        public bool Equals(Message other)
        {
            return this.Name == other.Name && this.Content == other.Content;
        }
    }
}