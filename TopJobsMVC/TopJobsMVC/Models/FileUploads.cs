using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TopJobsMVC.Models
{
    public class FileUploads
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public DateTime UploadedOn { get; set; }
    }
}