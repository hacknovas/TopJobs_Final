using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopJobsAPI.Entities;

namespace TopJobsAPI.Repositories
{
    public class JobPostsRepository : IJobPostsRepository
    {
        private TopJobContext _context;

        public JobPostsRepository()
        {
            _context = new TopJobContext();
        }
        public JobPosts GetJobPost(int id)
        {
            var posts= _context.JobPosts.SingleOrDefault(x => x.JobPostId == id);
            if (posts != null)
            {
                return posts;
            }
            return null;
        }
        public JobPosts AddJobPost(JobPosts jobPost)
        {
            _context.JobPosts.Add(jobPost);
            _context.SaveChanges();
            return jobPost;
        }

        public JobPosts DeleteJobPost(int id)
        {
            var jobPost = _context.JobPosts.SingleOrDefault(j => j.JobPostId == id);

            var AllJobAppl = _context.JobApplications.Where(x => x.JobPostId == id);
            var jobApplication = _context.JobApplications.RemoveRange(AllJobAppl);

            if (jobPost == null)
            {
                throw new InvalidOperationException($"JobPost with ID {id} not found.");
            }

            _context.JobPosts.Remove(jobPost);
            _context.SaveChanges();
            return jobPost;
        }

        public JobPosts UpdateJobPost(JobPosts updatedJobPost)
        {
            var existingJobPost = _context.JobPosts.SingleOrDefault(x=> x.JobPostId == updatedJobPost.JobPostId);

            if (existingJobPost == null)
            {
                throw new InvalidOperationException($"JobPost with ID {updatedJobPost.JobPostId} not found.");
            }

            existingJobPost.JobTitle = updatedJobPost.JobTitle;
            existingJobPost.JobDetails = updatedJobPost.JobDetails;
            existingJobPost.VacancyDetails = updatedJobPost.VacancyDetails;
            existingJobPost.RequiredSkills = updatedJobPost.RequiredSkills;
            existingJobPost.Category = updatedJobPost.Category;
            existingJobPost.JobDetails= updatedJobPost.JobDetails;
            existingJobPost.CreatedDate = updatedJobPost.CreatedDate;
            existingJobPost.ExpiryDate = updatedJobPost.ExpiryDate;
            existingJobPost.Location= updatedJobPost.Location;
            existingJobPost.salary = updatedJobPost.salary;

            _context.SaveChanges();

            return existingJobPost;
        }

        public List<JobPosts> GetAllJobPosts()
        {
            return _context.JobPosts.ToList();
        }

        public List<JobPosts> GetJobPostsByEmployer(int id) { 
            return _context.JobPosts.Where(x=>x.EmployerId==id).ToList();
        }

        public JobPosts createJobPost(JobPosts jobPost)
        {
            var jobpost = _context.JobPosts.Add(jobPost);
            _context.SaveChanges();
            return jobpost;
        }
    }
}


