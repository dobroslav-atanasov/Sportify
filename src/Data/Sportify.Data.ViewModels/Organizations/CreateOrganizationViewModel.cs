namespace Sportify.Data.ViewModels.Organizations
{
    using System.ComponentModel.DataAnnotations;
    using Constants;

    public class CreateOrganizationViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [MinLength(ModelConstants.MinOrganizationAbbreviationLength, ErrorMessage = ModelConstants.OrganizationAbbreviationLengthErrorMessage)]
        [RegularExpression(ModelConstants.CreateOrganization_Regex_Abbreviation, ErrorMessage = ModelConstants.OrganizationAbbreviationInvalidSymbolsErrorMessage)]
        [Display(Name = ModelConstants.CreateOrganization_Display_Abbreviation)]
        public string Abbreviation { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [MinLength(ModelConstants.MinOrganizationNameLength, ErrorMessage = ModelConstants.OrganizationNameLengthErrorMessage)]
        [RegularExpression(ModelConstants.CreateOrganization_Regex_Name, ErrorMessage = ModelConstants.OrganizationNameInvalidSymbolsErrorMessage)]
        [Display(Name = ModelConstants.CreateOrganization_Display_Name)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = ModelConstants.CreateOrganization_Display_Description)]
        public string Description { get; set; }
    }
}