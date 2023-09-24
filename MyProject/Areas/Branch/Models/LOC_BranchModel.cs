using System.ComponentModel.DataAnnotations;

namespace MyProject.Areas.Branch.Models
{
    public class LOC_BranchModel
    {
        public int? BranchID { get; set; }
        [Required(ErrorMessage = "Branch Name required")]
        public string? BranchName { get; set; }
        [Required(ErrorMessage = "Branch Code required")]
        public string? BranchCode { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }

        public class BranchDropDownModel
        {
            public int BranchID { get; set; }
            public string? BranchName { get; set; }
        }
    }
}
