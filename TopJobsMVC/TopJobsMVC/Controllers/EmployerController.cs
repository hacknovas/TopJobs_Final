using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using TopJobsMVC.Models;
using static System.Net.Mime.MediaTypeNames;



namespace TopJobsMVC.Controllers
{
    [Authorize(Roles = "Employer, JobSeeker")]
    public class EmployerController : Controller
    {
        HttpClient _httpClient;

        public EmployerController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:55065/"); 
        }

        [HttpGet]
        public async Task<ActionResult> Dashboard()
        {
            if (Session["Employer"] == null)
            {
                HttpResponseMessage response = await _httpClient.GetAsync("api/employer/getbyuserid/" + ((User)Session["User"]).UserId);
                var result = await response.Content.ReadAsStringAsync();
                var Data = JsonConvert.DeserializeObject<Employer>(result);
                Session["Employer"] = Data;
            }

            return View();
        }

        [HttpGet]
        public async Task<ActionResult> ProfileDetail()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("api/employer/getbyemployerid/" + ((Employer)Session["Employer"]).EmployerId);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var employer = JsonConvert.DeserializeObject<Employer>(result);

                    return View(employer);
                }

                ViewBag.ErrorMessage = "Error fetching profile details.";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"An error occurred: {ex.Message}";
                return View();
            }
        }

        [HttpPost]
        public async Task<ActionResult> EditProfileDetails(Employer employer)
        {
            try
            {
                var jsonContent = JsonConvert.SerializeObject(employer);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");


                HttpResponseMessage response = await _httpClient.PutAsync("api/employer/edit/", content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var employerData = JsonConvert.DeserializeObject<Employer>(result);

                    return RedirectToAction("ProfileDetail");
                }

                ViewBag.ErrorMessage = "Error fetching profile details.";
                return RedirectToAction("Dashboard");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"An error occurred: {ex.Message}";
                return RedirectToAction("Dashboard");
            }
        }

        [HttpGet]
        public async Task<ActionResult> ViewJobPosts()
        {
            try
            {
                int id = ((Employer)Session["Employer"]).EmployerId;

                HttpResponseMessage response = await _httpClient.GetAsync($"api/jobpost/employer/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var jobPosts = JsonConvert.DeserializeObject<List<JobPost>>(result);
                    return View(jobPosts); 
                }

                ViewBag.ErrorMessage = "Error fetching job posts.";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"An error occurred: {ex.Message}";
                return View();
            }
        }
        [HttpGet]
        public ActionResult CreateJobPostUI()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateJobPost(JobPost jobPost)
        {
            jobPost.EmployerId = ((Employer)Session["Employer"]).EmployerId;

            var jsonContent = JsonConvert.SerializeObject(jobPost);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await _httpClient.PostAsync("api/jobpost/create", content);

                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.ErrorMessage = "Job post creation failed. Please try again.";
                    return View("CreateJobPostUI");
                }

                return RedirectToAction("ViewJobPosts");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred during job post creation: " + ex.Message;
                return View();
            }
        }

        [HttpGet]
        public async Task<ActionResult> SingleJobPost(int jobpostid)
        {


            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("api/jobpost/get/" + jobpostid);

                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.ErrorMessage = "Job post creation failed. Please try again.";
                    return View("Dashboard");
                }

                var result = await response.Content.ReadAsStringAsync();
                var jobPosts = JsonConvert.DeserializeObject<JobPost>(result);
                return View(jobPosts);

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred during job post creation: " + ex.Message;
                return View();
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateJobPost(JobPost jobPost)
        {

            var jsonContent = JsonConvert.SerializeObject(jobPost);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await _httpClient.PutAsync("api/jobpost/update", content);

                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.ErrorMessage = "Job post creation failed. Please try again.";
                    return View("Dashboard");
                }

                return RedirectToAction("ViewJobPosts");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred during job post creation: " + ex.Message;
                return View();
            }
        }

        [HttpGet]
        public async Task<ActionResult> DeleteJobPost(int jobpostid)
        {

            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync("api/jobpost/delete/" + jobpostid);

                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.ErrorMessage = "Job post creation failed. Please try again.";
                    return View("Dashboard");
                }

                return RedirectToAction("ViewJobPosts");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred during job post creation: " + ex.Message;
                return View();
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetRecievedApplication(int jobpostid)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"api/jobapplications/jobpostid/{jobpostid}");

                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.ErrorMessage = "Failed to retrieve job applications. Please try again.";
                    return View("Dashboard");
                }

                var result = await response.Content.ReadAsStringAsync();
                var jobAppl = JsonConvert.DeserializeObject<List<JobApplication>>(result);

                if (jobAppl == null || jobAppl.Count == 0)
                {
                    ViewBag.ErrorMessage = "No applications found for this job post.";
                }

                return View(jobAppl);
            }
            catch (Exception ex)
            {
                // Consider logging the exception for debugging
                ViewBag.ErrorMessage = "An error occurred while retrieving job applications: " + ex.Message;
                return View("Dashboard");
            }
        }

        [HttpPost]
        public async Task<ActionResult> ChangeApplicationStatus(int ApplicationId, JobStatus JobApprovedStatus)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"api/jobapplications/setstatus/{ApplicationId}/{JobApprovedStatus}");

                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.ErrorMessage = "Failed to change application status. Please try again.";
                    return View("Dashboard");
                }

                var result = await response.Content.ReadAsStringAsync();
                var jobAppl = JsonConvert.DeserializeObject<JobApplication>(result);

                return RedirectToAction("GetRecievedApplication", new { jobpostid = jobAppl.JobPostId });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while changing the application status: " + ex.Message;
                return View("Dashboard");
            }
        }


    }
}
