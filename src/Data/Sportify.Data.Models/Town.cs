namespace Sportify.Data.Models
{
    using Common;

    public class Town : BaseModel<int>
    {
        public string Name { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}