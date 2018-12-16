namespace Sportify.Data.ViewModels.Events
{
    public class EventViewModel
    {
        public int Id { get; set; }

        public string EventName { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        public string Organization { get; set; }

        public string Sport { get; set; }

        public string Discipline { get; set; }

        public string Town { get; set; }

        public string Venue { get; set; }

        public int NumberOfParticipants { get; set; }
    }
}
