namespace Sportify.Data.ViewModels.Sports
{
    using System.Collections.Generic;
    using Disciplines;

    public class SportViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageSportUrl { get; set; }
    }
}