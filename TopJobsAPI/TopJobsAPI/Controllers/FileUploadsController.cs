using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TopJobsAPI.Models;
using TopJobsAPI.Repositories;

namespace TopJobsAPI.Controllers
{
    [RoutePrefix("api/resume")]
    public class FileUploadsController : ApiController
    {
        private FileUploadRepository fileUploadRepository;
        public FileUploadsController()
        {
            fileUploadRepository = new FileUploadRepository();
        }

        [HttpPost, Route("add/")]
        public IHttpActionResult addResume(FileUploads fileUploads)
        {
            string id = fileUploadRepository.AddResume(fileUploads);
            if (id!=null)
            {
                return Ok(id);
            }

            return BadRequest();
        }

        [HttpGet, Route("get/{id}")]
        public IHttpActionResult GetResume(string id)
        {
            var FileUploadData = fileUploadRepository.GetResume(id);
            if (FileUploadData != null)
            {
                return Ok(FileUploadData);
            }

            return BadRequest();
        }
    }
}
