using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TopJobsAPI.Entities
{
    public enum UserRole
    {
        JobSeeker,
        Employer,
        Admin,
        None
    }
    public class Users
    {
        [Key]  
        public int UserId { get; set; }

        [Required]
        [StringLength(30)]  
        public string Username { get; set; }

        [Required]
        [StringLength(50)]  
        public string Password { get; set; }

        [Required]
        [EnumDataType(typeof(UserRole))]  
        public UserRole Role { get; set; }
    }
}