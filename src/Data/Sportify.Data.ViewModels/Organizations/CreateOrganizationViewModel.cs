using System.ComponentModel.DataAnnotations;

namespace Sportify.Data.ViewModels.Organizations
{
    public class CreateOrganizationViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [MinLength(3, ErrorMessage = Constants.Constants.OrganizationNameLengthErrorMessage)]
        [RegularExpression("[a-zA-z0-9-.*/_\\s]+", ErrorMessage = Constants.Constants.OrganizationNameInvalidSymbolsErrorMessage)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [RegularExpression("[a-zA-z0-9-.*/_\\s]+", ErrorMessage = Constants.Constants.OrganizationDescriptionInvalidSymbolsErrorMessage)]
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
