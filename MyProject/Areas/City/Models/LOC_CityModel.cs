using System.ComponentModel.DataAnnotations;

namespace MyProject.Areas.City.Models
{
    public class LOC_CityModel
    {
        public int? CityID { get; set; }
        public int StateID { get; set; }
        public int CountryID { get; set; }
        [Required(ErrorMessage = "City Name required")]
        public string? CityName { get; set; }
        [Required(ErrorMessage = "City Code required")]
        public string? CityCode { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }

        public class CityDropDownModel
        {
            public int CityID { get; set; }
            public string? CityName { get; set; }
        }
    }
}
