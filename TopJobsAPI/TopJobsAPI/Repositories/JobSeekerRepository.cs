using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TopJobsAPI.Controllers;
using TopJobsAPI.Entities;

namespace TopJobsAPI.Repositories
{
    public class JobSeekerRepository : IJobSeekerRepository
    {
        private TopJobContext _context;

        public JobSeekerRepository()
        {
            _context = new TopJobContext();
        }

        public JobSeekers EditDetails(JobSeekers jobseeker)
        {
            var jobSeekerData = _context.JobSeekers.SingleOrDefault(x => x.JobSeekerId == jobseeker.JobSeekerId);
            if (jobSeekerData != null)
            {
                jobSeekerData.Name = jobseeker.Name;
                jobSeekerData.PhoneNumber = jobseeker.PhoneNumber;
                jobSeekerData.Experience = jobseeker.Experience;
                jobSeekerData.Skills = jobseeker.Skills;
                jobSeekerData.Organisation = jobseeker.Organisation;
                jobSeekerData.About = jobseeker.About;
                jobSeekerData.Email = jobseeker.Email;
                jobSeekerData.Location = jobseeker.Location;
                jobSeekerData.Education = jobseeker.Education;

                _context.SaveChanges();

                return jobSeekerData;
            }

            return null;
        }

        public JobSeekers GetDetails(int jobseekerid)
        {
            var jobSeekerData = _context.JobSeekers.SingleOrDefault(x => x.JobSeekerId == jobseekerid);
            if (jobSeekerData != null)
            {
                return jobSeekerData;
            }

            return null;
        }

        public JobSeekers GetDetailsByUserId(int userId)
        {
            var jobSeekerData = _context.JobSeekers.SingleOrDefault(x => x.UserId == userId);
            if (jobSeekerData != null)
            {
                return jobSeekerData;
            }

            return null;
        }


        public JobSeekers Register(JobSeekers jobSeekers)
        {
            var jobSUser = _context.JobSeekers.Add(jobSeekers);
            if ( jobSUser!= null)
            {
                _context.SaveChanges();
                return jobSUser;
            }
            return null;
        }

        public JobSeekers UpdateResumeId(ResumeUpdate resumeUpdate)
        {
            var jobSeekerData = _context.JobSeekers.SingleOrDefault(x => x.JobSeekerId == resumeUpdate.jobSeekerId);
            jobSeekerData.ResumeId = resumeUpdate.Id;
            _context.SaveChanges();
            return jobSeekerData;
        }
    }
}
