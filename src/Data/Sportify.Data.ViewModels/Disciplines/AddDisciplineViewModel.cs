namespace Sportify.Data.ViewModels.Disciplines
{
    using System.ComponentModel.DataAnnotations;

    public class AddDisciplineViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Description")]
        public string Description { get; set; }


        [Required]
        [Display(Name = "Sport")]
        public int SportId { get; set; }
    }
}