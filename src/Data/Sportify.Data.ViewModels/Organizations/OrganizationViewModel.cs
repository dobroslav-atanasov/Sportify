namespace Sportify.Data.ViewModels.Organizations
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Constants;

    public class OrganizationViewModel : IEquatable<OrganizationViewModel>
    {
        public int Id { get; set; }

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

        public string President { get; set; }

        public bool Equals(OrganizationViewModel other)
        {
            return this.Id == other.Id && this.Abbreviation == other.Abbreviation && 
                   this.Name == other.Name && this.Description == other.Description;
        }
    }
}