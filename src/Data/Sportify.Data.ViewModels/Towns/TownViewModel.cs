namespace Sportify.Data.ViewModels.Towns
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Constants;

    public class TownViewModel : IEquatable<TownViewModel>
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [MinLength(ModelConstants.MinTownNameLength, ErrorMessage = ModelConstants.TownNameLengthErrorMessage)]
        [RegularExpression(ModelConstants.AddTown_Regex_Name, ErrorMessage = ModelConstants.TownInvalidSymbolsErrorMessage)]
        [Display(Name = ModelConstants.AddTown_Display_Name)]
        public string Name { get; set; }

        [Required]
        [Display(Name = ModelConstants.AddTown_Display_Country)]
        public int CountryId { get; set; }

        public string CountryName { get; set; }
        public bool Equals(TownViewModel other)
        {
            return this.Id == other.Id && this.Name == other.Name && this.CountryId == other.CountryId;
        }
    }
}