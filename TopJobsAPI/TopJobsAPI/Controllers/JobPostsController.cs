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
    [RoutePrefix("api/jobpost")]
    public class JobPostsController : ApiController
    {
        private readonly IJobPostsRepository jobpostrepo;

        public JobPostsController()
        {
            jobpostrepo = new JobPostsRepository();
        }

        [HttpPost, Route("create/")]
        public IHttpActionResult CreatePostDetails([FromBody] JobPosts jobPosts)
        {
            var posts = jobpostrepo.createJobPost(jobPosts);
            if (posts != null)
            {
                return Ok(posts);
            }

            return BadRequest();
        }

        [HttpGet, Route("getall/")]
        public IHttpActionResult GetPostDetails()
        {
            var posts = jobpostrepo.GetAllJobPosts();
            if (posts != null)
            {
                return Ok(posts);
            }

            return BadRequest();
        }

        [HttpGet, Route("get/{id}")]
        public IHttpActionResult GetPostDetails(int id)
        {
            var post = jobpostrepo.GetJobPost(id);
            if (post != null)
            {
                return Ok(post);
            }

            return BadRequest();
        }

        [HttpPost, Route("add")]
        public IHttpActionResult AddPostDetails([FromBody] JobPosts jobpost)
        {
            var post = jobpostrepo.AddJobPost(jobpost);
            if (post != null)
            {
                return Ok(post);
            }

            return BadRequest();
        }

        [HttpDelete, Route("delete/{id}")]
        public IHttpActionResult DeletePostDetails(int id)
        {
            var post = jobpostrepo.DeleteJobPost(id);
            if (post != null)
            {
                return Ok(post);
            }

            return BadRequest();
        }

        [HttpPut, Route("update")]
        public IHttpActionResult UpdatePostDetails([FromBody] JobPosts jobpost)
        {
            var post = jobpostrepo.UpdateJobPost(jobpost);
            if (post != null)
            {
                return Ok(post);
            }

            return BadRequest();
        }


        [HttpGet, Route("employer/{id}")]
        public IHttpActionResult GetPostDetailsByEmployer(int id)
        {
            var post = jobpostrepo.GetJobPostsByEmployer(id);
            if (post != null)
            {
                return Ok(post);
            }

            return BadRequest();
        }


    }
}
