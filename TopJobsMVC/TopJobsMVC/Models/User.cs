using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace TopJobsMVC.Models
{
    public enum UserRole
    {
        JobSeeker,
        Employer,
        Admin,
        None
    }
    public class User
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Username must be between 5 and 50 characters long")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d).+$", ErrorMessage = "Password must contain at least one uppercase letter and one number")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public UserRole Role { get; set; }
    }
}