using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopJobsAPI.Entities;

namespace TopJobsAPI.Repositories
{
    internal interface IJobPostsRepository
    {
        JobPosts AddJobPost(JobPosts jobPost);
        JobPosts DeleteJobPost(int id);
        JobPosts UpdateJobPost(JobPosts updatedJobPost);
        JobPosts GetJobPost(int id);
        List<JobPosts> GetAllJobPosts();
        List<JobPosts> GetJobPostsByEmployer(int id);

        JobPosts createJobPost(JobPosts jobPost);
    }
}
