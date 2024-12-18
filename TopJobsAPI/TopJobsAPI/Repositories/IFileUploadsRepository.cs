using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopJobsAPI.Models;

namespace TopJobsAPI.Repositories
{
    internal interface IFileUploadsRepository
    {
        string AddResume(FileUploads fileUploads);
        FileUploads GetResume(string id);
    }
}
