﻿using System.ComponentModel.DataAnnotations;

namespace MyProject.Areas.Student.Models
{
    public class MST_StudentModel
    {
        public int? StudentID { get; set; }
        [Required(ErrorMessage = "Branch Name is required.")]        
        public int BranchID { get; set; }
        [Required(ErrorMessage = "City Name is required.")]
        public int CityID { get; set; }
        [Required(ErrorMessage = "Student Name is required.")]
        public string StudentName { get; set;}
        [Required(ErrorMessage = "Student Phone Number is required.")]
        public string MobileNoStudent { get; set; }
        [Required(ErrorMessage = "Student Email is required.")]
        [EmailAddress()]
        public string Email { get; set; }
        public string? MobileNoFather { get; set; }
        public string? Address { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? Age { get; set; }
        public bool IsActive { get; set; }
        public string? Gender { get; set; }
        public string? Password { get; set;}
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}
