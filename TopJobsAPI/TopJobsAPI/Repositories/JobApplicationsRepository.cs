using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TopJobsAPI.Entities;
using static System.Net.Mime.MediaTypeNames;

namespace TopJobsAPI.Repositories
{
    public class JobApplicationsRepository : IJobApplicationsRepository
    {
        private TopJobContext _context;

        public JobApplicationsRepository()
        {
            _context = new TopJobContext();
        }

        public JobApplications ApplyJob(JobApplications jobApplications)
        {
            var AllJobsByJobSeekerId = _context.JobApplications.Where(x => x.JobSeekerId == jobApplications.JobSeekerId);

            if (AllJobsByJobSeekerId.Any(x => x.JobPostId == jobApplications.JobPostId))
            {
                return null;
            }

            var application = _context.JobApplications.Add(jobApplications);
            _context.SaveChanges();
            return application;
        }

        public List<JobApplications> DisplayJobs()
        {
            return _context.JobApplications.ToList();
        }

        public List<JobApplications> DisplayJobsByApplicationId(int applicationId)
        {
            try
            {
                var jobApplication = _context.JobApplications.Where(x => x.ApplicationId == applicationId).ToList();
                return jobApplication;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<JobApplications> DisplayJobsByJobPostId(int jobPostId)
        {
            try
            {
                var jobApplication = _context.JobApplications.Where(x => x.JobPostId == jobPostId).ToList();
                return jobApplication;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<JobApplications> DisplayJobsByJobSeekerId(int jobSeekerId)
        {
            try
            {
                var jobApplication = _context.JobApplications.Where(x => x.JobSeekerId == jobSeekerId).ToList();
                return jobApplication;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public JobApplications SetJobApplicationStatus(int id, JobStatus status)
        {
            try
            {
                var jobApplication = _context.JobApplications.SingleOrDefault(x => x.ApplicationId == id);

                jobApplication.JobApprovedStatus = status;

                _context.SaveChanges();
                return jobApplication;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public JobApplications WithdrawJobApplication(int applicationId)
        {
            var jobApplication = _context.JobApplications.SingleOrDefault(x => x.ApplicationId == applicationId);
            var removedApplication = _context.JobApplications.Remove(jobApplication);
            _context.SaveChanges();
            return removedApplication;

        }
    }
}
