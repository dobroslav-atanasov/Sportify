namespace Sportify.Data.ViewModels.Participants
{
    using System;
    using Models;

    public class ParticipantViewModel
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public int EventId { get; set; }

        public Event Event { get; set; }

        public DateTime? Result { get; set; }
    }
}