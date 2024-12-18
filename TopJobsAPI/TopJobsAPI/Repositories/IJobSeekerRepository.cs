using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopJobsAPI.Controllers;
using TopJobsAPI.Entities;

namespace TopJobsAPI.Repositories
{
    internal interface IJobSeekerRepository
    {
        JobSeekers Register(JobSeekers jobSeekers);
        JobSeekers EditDetails(JobSeekers jobseeker);
        JobSeekers GetDetails(int jobseekerid);
        JobSeekers GetDetailsByUserId(int userId);
        JobSeekers UpdateResumeId(ResumeUpdate resumeUpdate);
    }
}
