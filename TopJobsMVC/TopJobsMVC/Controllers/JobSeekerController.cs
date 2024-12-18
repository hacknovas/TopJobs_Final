using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using TopJobsMVC.Models;

namespace TopJobsMVC.Controllers
{
    [Authorize(Roles = "Admin, JobSeeker")]
    [RoutePrefix("jobseeker")]
    public class JobSeekerController : Controller
    {
        HttpClient _httpClient;
        public JobSeekerController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:55065/");
        }

        [Route("dashboard")]
        [HttpGet]
        public async Task<ActionResult> Dashboard()
        {
            if (Session["JobSeeker"] == null)
            {

                HttpResponseMessage response = await _httpClient.GetAsync("api/jobseeker/detailsbyuserid/" + ((User)Session["User"]).UserId);
                var result = await response.Content.ReadAsStringAsync();
                var Data = JsonConvert.DeserializeObject<JobSeeker>(result);
                Session["JobSeeker"] = Data;
            }

            return View();
        }

        [Route("adddetails")]
        [HttpGet]
        public ActionResult AddDetails()
        {

            return View();
        }

        public async Task<ActionResult> GetJobSProfile()
        {
            int userId = ((User)Session["User"]).UserId;

            HttpResponseMessage response = await _httpClient.GetAsync("api/jobseeker/detailsbyuserid/" + userId);

            var result = await response.Content.ReadAsStringAsync();
            var Data = JsonConvert.DeserializeObject<JobSeeker>(result);

            Session["JobSeeker"] = Data;

            return View("JobSProfile", Data);
        }


        public ActionResult JobSProfile(JobSeeker jobSeeker)
        {
            return View(jobSeeker);
        }

        public async Task<ActionResult> UpdateJobSProfile(JobSeeker jobSeeker)
        {
            var jsonContent = JsonConvert.SerializeObject(jobSeeker);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync("api/jobseeker/editdetails", content);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.ErrorMessage = "Details not updated. Please try again.";
                return RedirectToAction("GetJobSProfile");
            }

            var res = await response.Content.ReadAsStringAsync();
            var Data = JsonConvert.DeserializeObject<JobSeeker>(res);

            Session["JobSeeker"] = Data;
            return View("JobSProfile", Data);
        }

        public async Task<ActionResult> GetAllJobPost()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("api/jobpost/getall");

            if (!response.IsSuccessStatusCode)
            {
                return View("Dashboard");
            }

            var result = await response.Content.ReadAsStringAsync();
            var Data = JsonConvert.DeserializeObject<List<JobPost>>(result);

            return View(Data);
        }

        public async Task<ActionResult> ApplyJob(int jobPostId)
        {
            JobApplication newApplication = new JobApplication();
            newApplication.JobPostId = jobPostId;

            var JobSeekerId = ((JobSeeker)Session["JobSeeker"]).JobSeekerId;

            newApplication.JobSeekerId = JobSeekerId;

            var jsonContent = JsonConvert.SerializeObject(newApplication);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("api/jobapplications/apply", content);


            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("JobsApplied");
            }

            var result = await response.Content.ReadAsStringAsync();
            var Data = JsonConvert.DeserializeObject<JobApplication>(result);

            return RedirectToAction("JobsApplied");
        }

        public async Task<ActionResult> JobsApplied()
        {
            var JobSeekerId = ((JobSeeker)Session["JobSeeker"]).JobSeekerId;

            HttpResponseMessage response = await _httpClient.GetAsync("api/jobapplications/jobseekerid/" + JobSeekerId);

            var result = await response.Content.ReadAsStringAsync();
            var Data = JsonConvert.DeserializeObject<List<JobApplication>>(result);

            if (Data!=null)
            {
                return View(Data);
            }
            return View("Dashboard");
        }

        public async Task<ActionResult> GetSingleJobPost(int jobPostId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync("api/jobpost/get/" + jobPostId);

            if (!response.IsSuccessStatusCode)
            {
                return View("Dashboard");
            }

            var result = await response.Content.ReadAsStringAsync();
            var Data = JsonConvert.DeserializeObject<JobPost>(result);

            return View(Data);
        }


        public async Task<ActionResult> WithdrawApplication(int applicationId)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync("api/jobapplications/delete/" + applicationId);

            if (!response.IsSuccessStatusCode)
            {
                return View("Dashboard");
            }

            return RedirectToAction("JobsApplied");
        }
    }
}
