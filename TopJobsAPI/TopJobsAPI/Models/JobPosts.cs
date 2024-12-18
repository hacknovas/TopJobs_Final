using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TopJobsAPI.Entities
{
    public class JobPosts
    {

        [Key]
        public int JobPostId { get; set; }
        public int EmployerId { get; set; }
        [ForeignKey("EmployerId")]
        public virtual Employers Employers { get; set; }
        public string JobTitle { get; set; }
        public string JobDetails { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string VacancyDetails { get; set; } 
        public int salary { get; set; }
        public string RequiredSkills { get; set; }
        public string Location { get;set; }
        public string Category { get; set; }
    }
}