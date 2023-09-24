using System.ComponentModel.DataAnnotations;

namespace MyProject.Areas.State.Models
{
    public class LOC_StateModel
    {
        public int? StateID { get; set; }
        [Required(ErrorMessage = "Country Name is Required.")]
        public int? CountryID { get; set; }
        [Required(ErrorMessage = "State Name is Required.")]
        public string? StateName { get; set; }
        [Required(ErrorMessage = "State Code is Required.")]
        public string? StateCode { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }

        public class StateDropDownModel
        {
            public int StateID { get; set; }
            public string StateName { get; set; }
        }
    }
}
