using Antlr.Runtime.Misc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TopJobsMVC.Models;

namespace TopJobsMVC.Controllers
{

    [Authorize(Roles = "Admin")]
    [RoutePrefix("admin")]
    public class AdminController : Controller
    {
        HttpClient client;
        public AdminController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:55065/");
        }

        public ActionResult Dashboard()
        {
            return View();
        }
       
        public async Task<ActionResult> GetAllJobs()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("api/jobpost/getall/");

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    List<JobPost> jobposts = JsonConvert.DeserializeObject<List<JobPost>>(responseContent);

                    return View(jobposts);
                }
                else
                {
                    return View("Error", new { Message = "Unable to retrieve job posts." });
                }
            }
            catch (Exception ex)
            {
                return View("Error", new { Message = ex.Message });
            }
        }
        public async Task<ActionResult> DeleteJob(int id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync($"api/jobpost/delete/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetAllJobs");
                }
                else
                {
                    return View("Error", new { Message = "Unable to delete the job post." });
                }
            }
            catch (Exception ex)
            {
                return View("Error", new { Message = ex.Message });
            }
        }
        public async Task<ActionResult> DetailsJob(int id)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"api/jobpost/get/{id}");

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    JobPost jobPost = JsonConvert.DeserializeObject<JobPost>(responseContent);

                    return View(jobPost);
                }
                else
                {
                    return View("Error", new { Message = "Unable to retrieve job details." });
                }
            }
            catch (Exception ex)
            {
                return View("Error", new { Message = ex.Message });
            }
        }

        public async Task<ActionResult> GetEmployerBySearch(int searchEmployer)
        {

            string apiUrl = $"api/employer/getbyemployerid/{searchEmployer}";

            HttpResponseMessage response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var employers = await response.Content.ReadAsStringAsync();
                var Data=JsonConvert.DeserializeObject<Employer>(employers);
                return View(Data); 
            }

            ViewBag.ErrorMessage = "Could not retrieve employers.";
            return View("Dashboard");
        }


        //public async Task<ActionResult> DeleteEmployer(int id)
        //{

        //    HttpResponseMessage response = await client.DeleteAsync("api/employer/delete/"+id);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        return RedirectToAction("Dashboard");
        //    }

        //    ViewBag.ErrorMessage = "Could not retrieve employers.";
        //    return RedirectToAction("Dashboard");
        //}


        public async Task<ActionResult> GetJobSeekerBySearch(int searchJobSeeker)
        {

            string apiUrl = $"api/jobseeker/getdetails/{searchJobSeeker}";

            HttpResponseMessage response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var jobseek = await response.Content.ReadAsStringAsync();
                var Data = JsonConvert.DeserializeObject<JobSeeker>(jobseek);
                return View(Data);
            }

            ViewBag.ErrorMessage = "Could not retrieve employers.";
            return View("Dashboard");
        }
    }
    }


