using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TopJobsMVC.Models
{
    public class JobPost
    {
        public int JobPostId { get; set; }

        public int EmployerId { get; set; }

        [ForeignKey("EmployerId")]
        public virtual Employer Employers { get; set; }

        [Required(ErrorMessage = "Job Title is required")]
        [StringLength(100, ErrorMessage = "Job Title cannot be longer than 100 characters")]
        public string JobTitle { get; set; }

        [Required(ErrorMessage = "Job Details are required")]
        [StringLength(1000, ErrorMessage = "Job Details cannot be longer than 1000 characters")]
        public string JobDetails { get; set; }

        [Required(ErrorMessage = "Creation Date is required")]
        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "Expiry Date is required")]
        public DateTime ExpiryDate { get; set; }

        [StringLength(500, ErrorMessage = "Vacancy Details cannot be longer than 500 characters")]
        public string VacancyDetails { get; set; }

        [Required(ErrorMessage = "Salary is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Salary must be a positive number")]
        public int salary { get; set; }

        [StringLength(500, ErrorMessage = "Required Skills cannot be longer than 500 characters")]
        public string RequiredSkills { get; set; }

        [StringLength(100, ErrorMessage = "Location cannot be longer than 100 characters")]
        public string Location { get; set; }

        [StringLength(50, ErrorMessage = "Category cannot be longer than 50 characters")]
        public string Category { get; set; }
    }
}
