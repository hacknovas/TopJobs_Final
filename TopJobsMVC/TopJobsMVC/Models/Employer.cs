using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TopJobsMVC.Models
{
    public class Employer
    {
        public int EmployerId { get; set; }

        public int UserId { get; set; }

        [Required(ErrorMessage = "Employer name is required")]
        [StringLength(100, ErrorMessage = "Employer name cannot be longer than 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Contact number is required")]
        [StringLength(15, ErrorMessage = "Contact number cannot be longer than 15 characters")]
        [RegularExpression(@"^\d{10,15}$", ErrorMessage = "Contact number must be between 10 and 15 digits")]
        public string ContactNumber { get; set; }

        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [StringLength(150, ErrorMessage = "Organisation name cannot be longer than 150 characters")]
        public string Organisation { get; set; }

        [StringLength(500, ErrorMessage = "Organisation details cannot be longer than 500 characters")]
        public string OrganisationDetails { get; set; }
    }
}
