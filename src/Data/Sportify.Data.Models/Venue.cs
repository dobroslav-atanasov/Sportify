namespace Sportify.Data.Models
{
    using Common;

    public class Venue : BaseModel<int>
    {
        public string Name { get; set; }

        public int Capacity { get; set; }

        public string ImageVenueUrl { get; set; }

        public int TownId { get; set; }
        public Town Town { get; set; }
    }
}