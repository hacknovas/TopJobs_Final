using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using TopJobsMVC.Models;

namespace TopJobsMVC.Controllers
{
    public class FileUploadsController : Controller
    {
        private readonly HttpClient _httpClient;

        public FileUploadsController()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:55065/") // Ensure correct API address
            };
        }

        // File upload action
        [HttpPost]
        public async Task<ActionResult> Index(HttpPostedFileBase file,int jobSeekerId)
        {
            if (file == null || file.ContentLength == 0)
            {
                // If no file is uploaded, redirect back to the profile page
                return RedirectToAction("GetJobSProfile", "JobSeeker");
            }

            try
            {
                var extension = Path.GetExtension(file.FileName);
                var fileName = GenerateFileName(extension);
                var path = Path.Combine(Server.MapPath("~/UploadedFiles"), fileName);

                // Save the file to the server
                file.SaveAs(path);

                var fileUpload = new FileUploads
                {
                    Id= Guid.NewGuid().ToString(),
                    FileName = fileName,
                    FileUrl = Url.Content(Path.Combine("~/UploadedFiles", fileName)),
                    UploadedOn = DateTime.Now
                };

                // Save file details to the database and get the saved data
                var fileData = await SaveFileDetailsToDatabase(fileUpload);


                var data = new ResumeUpdate();
                data.jobSeekerId = jobSeekerId;
                data.Id = fileUpload.Id;

                //if (fileData==0)
                //{
                //    data.Id = 3000;

                //}


                // Serialize jobseeker data and send update request to API
                var jsonContent = JsonConvert.SerializeObject(data);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PutAsync("api/jobseeker/UpdateResume/", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetJobSProfile", "JobSeeker"); // Success - redirect to profile
                }
                else
                {
                    // Handle API failure, maybe log or notify user
                    TempData["Error"] = "Failed to update resume.";
                    return RedirectToAction("Dashboard", "JobSeeker");
                }
            }
            catch (Exception ex)
            {
                // Log the exception and redirect to profile
                // You can implement proper logging here, for example, using NLog or Serilog
                TempData["Error"] = "An error occurred during file upload.";
                return RedirectToAction("GetJobSProfile", "User");
            }
        }



        // Generate a unique file name based on current timestamp
        private string GenerateFileName(string extension)
        {
            return $"my-file-{DateTime.Now:yyyyMMddHHmmssfff}{extension}";
        }

        // Save file details to the database
        private async Task<string> SaveFileDetailsToDatabase(FileUploads fileUpload)
        {
            var jsonContent = JsonConvert.SerializeObject(fileUpload);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await _httpClient.PostAsync("api/resume/add/", content);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Failed to save file details to the database.");
                }

                var result = await response.Content.ReadAsStringAsync();
                    var id=JsonConvert.DeserializeObject<string>(result);
                return id;
            }
            catch (Exception ex)
            {
                // Log error and handle the exception
                throw new Exception("Error occurred while saving file details to the database.", ex);
            }
        }

        // Download the uploaded file
        // Download the uploaded file
        public async Task<ActionResult> DisplayFile(string id)
        {
            try
            {
                var fileData = await FetchFileDetailsFromApi(id);

                if (fileData == null)
                {
                    return HttpNotFound(); // File not found
                }

                var fullPath = Server.MapPath("~" + fileData.FileUrl);

                if (!System.IO.File.Exists(fullPath))
                {
                    return HttpNotFound(); // If the file is not found
                }

                // Return the file as a response so the browser can render it
                byte[] fileBytes = System.IO.File.ReadAllBytes(fullPath);
                return File(fileBytes, "application/pdf");
            }
            catch (Exception ex)
            {
                // Log the error
                TempData["Error"] = "An error occurred while displaying the file.";
                return HttpNotFound();
            }
        }


        // Fetch file details from the API based on file ID
        private async Task<FileUploads> FetchFileDetailsFromApi(string id)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"api/resume/get/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<FileUploads>(result);
                }
            }
            catch (Exception ex)
            {
                // Log error and handle the exception
                throw new Exception("Error occurred while fetching file details from API.", ex);
            }

            return null;
        }
    }
    public class ResumeUpdate
    {
        public string Id { get; set; }
        public int jobSeekerId { get; set; }
    }
}
