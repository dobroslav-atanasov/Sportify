namespace Sportify.Data.ViewModels.Disciplines
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Constants;

    public class DisciplineViewModel : IEquatable<DisciplineViewModel>
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = ModelConstants.AddDiscipline_Display_Name)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = ModelConstants.AddDiscipline_Display_Description)]
        public string Description { get; set; }

        [Required]
        [Display(Name = ModelConstants.AddDiscipline_Display_Sport)]
        public int SportId { get; set; }

        public string Sport { get; set; }

        public bool Equals(DisciplineViewModel other)
        {
            return this.Id == other.Id && this.Name == other.Name && 
                   this.Description == other.Description && this.SportId == other.SportId;
        }
    }
}