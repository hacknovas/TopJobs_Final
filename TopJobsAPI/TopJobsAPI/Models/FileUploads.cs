using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TopJobsAPI.Models
{
    public class FileUploads
    {
        [Key]
        public string Id { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public DateTime UploadedOn { get; set; }
    }
}