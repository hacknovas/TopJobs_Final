using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TopJobsAPI.Entities;
using TopJobsAPI.Repositories;

namespace TopJobsAPI.Controllers
{
    [RoutePrefix("api/jobseeker")]

    public class JobSeekerController : ApiController
    {
        private JobSeekerRepository _jobSeekerRepository;
        public JobSeekerController()
        {
            _jobSeekerRepository = new JobSeekerRepository();
        }

        [HttpPost, Route("register")]
        public IHttpActionResult AddJobSeeker([FromBody] JobSeekers jobseeker)
        {
            var jobSekerUser = _jobSeekerRepository.Register(jobseeker);
            if (jobSekerUser != null)
            {
                return Ok(jobSekerUser);
            }
            return BadRequest("Registration failed. Please check the provided details.");
        }

        [HttpGet, Route("getdetails/{jobseekerid}")]
        public IHttpActionResult GetDetailsJobSeeker(int jobseekerid)
        {
            var jobSeekerdata = _jobSeekerRepository.GetDetails(jobseekerid);

            return Ok(jobSeekerdata);
        }

        [HttpGet, Route("detailsbyuserid/{userid}")]
        public IHttpActionResult GetDetailsJobSeekerByUserId(int userid)
        {
            var jobSeekerdata = _jobSeekerRepository.GetDetailsByUserId(userid);

            return Ok(jobSeekerdata);
        }

        [HttpPut, Route("editdetails")]
        public IHttpActionResult EditDetailsJobSeeker([FromBody] JobSeekers jobseeker)
        {
            var jobSeekerData = _jobSeekerRepository.EditDetails(jobseeker);
            if (jobSeekerData != null)
            {
                return Ok(jobSeekerData);
            }
            return BadRequest("Details Not Updated");
        }

        [HttpPut, Route("UpdateResume/")]
        public IHttpActionResult UpdateResume([FromBody] ResumeUpdate resumeUpdate)
        {
            var jobSeekerData = _jobSeekerRepository.UpdateResumeId(resumeUpdate);
            if (jobSeekerData != null)
            {
                return Ok(jobSeekerData);
            }
            return BadRequest("Resume Details Not Updated");
        }
    }


    public class ResumeUpdate
    {
        public string Id { get; set; }
        public int jobSeekerId { get; set; }
    }
}