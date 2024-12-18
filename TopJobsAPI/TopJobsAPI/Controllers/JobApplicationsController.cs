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
    [RoutePrefix("api/jobapplications")]
    public class JobApplicationsController : ApiController
    {
        private IJobApplicationsRepository _jobApplicationsRepository;

        public JobApplicationsController()
        {
            _jobApplicationsRepository = new JobApplicationsRepository();
        }

        [HttpPost]
        [Route("apply")]
        public IHttpActionResult ApplyToJob(JobApplications jobApplications)
        {
            try
            {
                var _jobApplications = _jobApplicationsRepository.ApplyJob(jobApplications);
                if (_jobApplications!=null)
                {
                    return Ok(_jobApplications);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }


        [HttpGet]
        [Route("applicationid/{applicationId}")]
        public IHttpActionResult DisplayJobsByApplicationId(int applicationId)
        {
            try
            {
                var jobApplications = _jobApplicationsRepository.DisplayJobsByApplicationId(applicationId);
                if (jobApplications.Count == 0)
                {
                    return NotFound();
                }
                return Ok(jobApplications);
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("jobpostid/{jobPostId}")]
        public IHttpActionResult DisplayJobsByJobPostId(int jobPostId)
        {
            try
            {
                var jobApplications = _jobApplicationsRepository.DisplayJobsByJobPostId(jobPostId);
                
                return Ok(jobApplications);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("jobseekerid/{jobSeekerId}")]
        public IHttpActionResult DisplayJobsByJobSeekerId(int jobSeekerId)
        {
            try
            {
                var jobApplications = _jobApplicationsRepository.DisplayJobsByJobSeekerId(jobSeekerId);
                if (jobApplications.Count == 0)
                {
                    return NotFound();
                }
                return Ok(jobApplications);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }



        [HttpGet]
        [Route("setstatus/{id}/{status}")]
        public IHttpActionResult SetJobApplicationStatus(int id, JobStatus status)
        {
            try
            {
                var jobApplication = _jobApplicationsRepository.SetJobApplicationStatus(id,status);
                return Ok(jobApplication);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IHttpActionResult DeleteApplication(int id)
        {
            try
            {
                var jobApplication = _jobApplicationsRepository.WithdrawJobApplication(id);
                return Ok(jobApplication);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
