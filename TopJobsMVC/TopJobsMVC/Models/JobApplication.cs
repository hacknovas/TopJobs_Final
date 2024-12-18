using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TopJobsMVC.Models
{

    public enum JobStatus
    {
        Pending,
        Accepted,
        Rejected
    }
    public class JobApplication
    {
        public int ApplicationId { get; set; }
        [Required]
        public int JobPostId { get; set; }
        [ForeignKey("JobPostId")]
        public virtual JobPost JobPosts { get; set; }
        [Required]
        public int JobSeekerId { get; set; }
        [ForeignKey("JobSeekerId")]
        public virtual JobSeeker JobSeekers { get; set; }
        public JobStatus JobApprovedStatus { get; set; }
    }
}