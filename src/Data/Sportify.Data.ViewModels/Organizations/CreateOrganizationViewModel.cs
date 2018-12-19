namespace Sportify.Data.ViewModels.Organizations
{
    using System.ComponentModel.DataAnnotations;
    using Constants;

    public class CreateOrganizationViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [MinLength(Constants.MinOrganizationNameLength, ErrorMessage = Constants.OrganizationNameLengthErrorMessage)]
        [RegularExpression(Constants.CreateOrganization_Regex_Name, ErrorMessage = Constants.OrganizationNameInvalidSymbolsErrorMessage)]
        [Display(Name = Constants.CreateOrganization_Display_Name)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [RegularExpression(Constants.CreateOrganization_Regex_Description, ErrorMessage = Constants.OrganizationDescriptionInvalidSymbolsErrorMessage)]
        [Display(Name = Constants.CreateOrganization_Display_Description)]
        public string Description { get; set; }
    }
}