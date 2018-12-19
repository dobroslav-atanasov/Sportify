namespace Sportify.Data.ViewModels.Sports
{
    using System;

    public class SportViewModel : IEquatable<SportViewModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageSportUrl { get; set; }

        public bool Equals(SportViewModel other)
        {
            return this.Id == other.Id 
                   && this.Name == other.Name 
                   && this.Description == other.Description 
                   && this.ImageSportUrl == other.ImageSportUrl;
        }
    }
}