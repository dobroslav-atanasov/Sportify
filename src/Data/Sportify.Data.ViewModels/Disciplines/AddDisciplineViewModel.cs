namespace Sportify.Data.ViewModels.Disciplines
{
    using System.ComponentModel.DataAnnotations;
    using Constants;

    public class AddDisciplineViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = Constants.AddDiscipline_Display_Name)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = Constants.AddDiscipline_Display_Description)]
        public string Description { get; set; }
        
        [Required]
        [Display(Name = Constants.AddDiscipline_Display_Sport)]
        public int SportId { get; set; }
    }
}