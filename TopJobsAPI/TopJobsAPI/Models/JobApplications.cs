using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TopJobsAPI.Entities
{
    public enum JobStatus
    {
        Pending,
        Accepted,
        Rejected
    }
    public class JobApplications
    {
        [Key]
        public int ApplicationId { get; set; }
        [Required]
        public int JobPostId { get; set; }
        [ForeignKey("JobPostId")]
        public virtual JobPosts JobPosts { get; set; }
        [Required]
        public int JobSeekerId { get; set; }
        [ForeignKey("JobSeekerId")]
        public virtual JobSeekers JobSeekers { get; set; }
        public JobStatus JobApprovedStatus { get; set; } 
    }
}
