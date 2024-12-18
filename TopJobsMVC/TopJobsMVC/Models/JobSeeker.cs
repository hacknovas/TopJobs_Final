using System;
using System.ComponentModel.DataAnnotations;

namespace TopJobsMVC.Models
{
    public class JobSeeker
    {
        public int UserId { get; set; }
        public int JobSeekerId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters")]
        public string Name { get; set; }

        [StringLength(12, ErrorMessage = "Phone number cannot be longer than 12 characters")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Resume is required")]
        public string ResumeId { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [StringLength(500, ErrorMessage = "Skills description cannot be longer than 500 characters")]
        public string Skills { get; set; }

        [StringLength(1000, ErrorMessage = "About section cannot be longer than 1000 characters")]
        public string About { get; set; }

        [StringLength(1000, ErrorMessage = "Experience description cannot be longer than 1000 characters")]
        public string Experience { get; set; }

        [StringLength(500, ErrorMessage = "Education description cannot be longer than 500 characters")]
        public string Education { get; set; }

        [StringLength(500, ErrorMessage = "Organisation description cannot be longer than 500 characters")]
        public string Organisation { get; set; }

        [StringLength(100, ErrorMessage = "Location cannot be longer than 100 characters")]
        public string Location { get; set; }
    }
}
