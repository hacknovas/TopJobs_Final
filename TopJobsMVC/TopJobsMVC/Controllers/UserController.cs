using Antlr.Runtime.Misc;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TopJobsMVC.Models;


namespace TopJobsMVC.Controllers
{
    [RoutePrefix("user")]
    public class UserController : Controller
    {

        HttpClient _httpClient;
        public UserController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:55065/");
        }

        [Route("login")]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [Route("login")]
        [HttpPost]
        public async Task<ActionResult> Login(User user)
        {
            var jsonContent = JsonConvert.SerializeObject(user);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await _httpClient.PostAsync("api/user/login/", content);

                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.ErrorMessage = "Login failed. Please try again.";
                    return View("Login");
                }

                var result = await response.Content.ReadAsStringAsync();
                var Data = JsonConvert.DeserializeObject<User>(result);

                Session["User"] = Data;
                FormsAuthentication.SetAuthCookie(Data.Role.ToString(), false);

                if (Data.Role == UserRole.Employer)
                {
                    return RedirectToAction("Dashboard", "Employer");
                }else if (Data.Role == UserRole.Admin)
                {
                    return RedirectToAction("Dashboard", "Admin");
                }

                return RedirectToAction("Dashboard", "JobSeeker");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred during login: " + ex.Message;
                return View("Login");
            }
        }

        [Route("register")]
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [Route("register")]
        [HttpPost]
        public async Task<ActionResult> Register(User user)
        {
            var jsonContent = JsonConvert.SerializeObject(user);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            

            try
            {
                HttpResponseMessage response = await _httpClient.PostAsync("api/user/register/", content);

                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.ErrorMessage = "Registration failed. Please try again.";
                    return RedirectToAction("Login");
                }

                var result = await response.Content.ReadAsStringAsync();
                var Data = JsonConvert.DeserializeObject<User>(result);

                if (Data.Role == UserRole.JobSeeker)
                {
                    var jsonContentJobSeeker = JsonConvert.SerializeObject(new JobSeeker() { Name = Data.Username, UserId = Data.UserId ,ResumeId="null"});
                    var contentJobSeeker = new StringContent(jsonContentJobSeeker, Encoding.UTF8, "application/json");
                    HttpResponseMessage responseJobSeeker = await _httpClient.PostAsync("api/jobseeker/register/", contentJobSeeker);
                }else if (Data.Role == UserRole.Employer)
                {
                    var jsonContentEmployer = JsonConvert.SerializeObject(new Employer() { UserId = Data.UserId });
                    var contentEmployer = new StringContent(jsonContentEmployer, Encoding.UTF8, "application/json");
                    HttpResponseMessage responseEmployer = await _httpClient.PostAsync("api/employer/register/", contentEmployer);
                }


                return RedirectToAction("Login");

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred during login: " + ex.Message;

                return RedirectToAction("Login");
            }
        
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Home", "Dashboard");
        }
    }
}
