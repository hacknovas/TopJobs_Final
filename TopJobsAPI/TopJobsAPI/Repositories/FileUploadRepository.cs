using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TopJobsAPI.Entities;
using TopJobsAPI.Models;

namespace TopJobsAPI.Repositories
{
    public class FileUploadRepository : IFileUploadsRepository
    {
        private TopJobContext _context;

        public FileUploadRepository()
        {
            _context = new TopJobContext();
        }

        public string AddResume(FileUploads fileUploads)
        {
            var fileUploadData=_context.FileUploads.Add(fileUploads);
           _context.SaveChangesAsync();
            return fileUploadData.Id;
        }

        public FileUploads GetResume(string id)
        {
            return _context.FileUploads.SingleOrDefault(file => file.Id == id);
        }
    }
}
