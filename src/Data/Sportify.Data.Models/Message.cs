namespace Sportify.Data.Models
{
    public class Message
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        public string MessageContent { get; set; }
    }
}