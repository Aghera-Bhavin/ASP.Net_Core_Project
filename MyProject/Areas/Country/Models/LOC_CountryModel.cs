using System.ComponentModel.DataAnnotations;

namespace MyProject.Areas.Country.Models
{
    public class LOC_CountryModel
    {
        public int? CountryID { get; set; }
        [Required(ErrorMessage = "Country Name required")]
        public string? CountryName { get; set; }
        [Required(ErrorMessage = "Country Code required")]
        public string? CountryCode { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }

        public class CountryDropDownModel
        {
            public int CountryID { get; set; }
            public string? CountryName { get; set; }
        }
    }
}
