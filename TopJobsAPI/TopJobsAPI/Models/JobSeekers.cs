using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TopJobsAPI.Entities
{
    public class JobSeekers
    {
        [Key]
        public int JobSeekerId { get; set; }

        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual Users User { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(12)]
        public string PhoneNumber { get; set; }
        
        [Required]
        public string ResumeId { get; set; } 
        public string Email { get; set; }
        public string Skills { get; set; }
        public string About { get;set; }
        public string Experience { get; set; }
        public string Education { get;set; }
        public string Organisation { get; set; }
        public string Location { get; set; }
    }
}