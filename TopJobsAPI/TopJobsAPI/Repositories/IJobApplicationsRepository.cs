using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopJobsAPI.Entities;

namespace TopJobsAPI.Repositories
{
    internal interface IJobApplicationsRepository
    {
        JobApplications ApplyJob(JobApplications jobApplications);
        List<JobApplications> DisplayJobs();
        List<JobApplications> DisplayJobsByApplicationId(int applicationId);
        List<JobApplications> DisplayJobsByJobPostId(int jobPostId);
        List<JobApplications> DisplayJobsByJobSeekerId(int jobSeekerId);
        JobApplications SetJobApplicationStatus(int id, JobStatus status);
        JobApplications WithdrawJobApplication(int  applicationId);   
    }
}
