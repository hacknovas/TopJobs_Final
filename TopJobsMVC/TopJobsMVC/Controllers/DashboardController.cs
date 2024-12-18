using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using TopJobsMVC.Models;

namespace TopJobsMVC.Controllers
{
    public class DashboardController : Controller
    {
        

        HttpClient _httpClient;
        public DashboardController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:55065/");
        }

        public ActionResult Home()
        {

            return View();
        }
    }
}
