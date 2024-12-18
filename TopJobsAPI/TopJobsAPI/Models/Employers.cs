using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TopJobsAPI.Entities
{
    public class Employers
    {
        [Key]
        public int EmployerId { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual Users User { get; set; }
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string Organisation { get; set; }
        public string OrganisationDetails { get;set; }
    }
}