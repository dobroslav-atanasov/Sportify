namespace Sportify.Data.Models
{
    using System;

    public class Message : IEquatable<Message>
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        public DateTime PublishedOn { get; set; } = DateTime.UtcNow;

        public bool Equals(Message other)
        {
            return this.FullName == other.FullName && this.Subject == other.Subject && this.Content == other.Content && this.UserId == other.UserId;
        }
    }
}