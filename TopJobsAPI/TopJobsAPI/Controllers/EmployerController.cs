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
    [RoutePrefix("api/employer")]
    public class EmployerController : ApiController
    {
        private readonly IEmployerRepository _employerRepo;

        public EmployerController()
        {
            _employerRepo = new EmployerRepository();
        }

        [HttpPut, Route("edit")]
        public IHttpActionResult EditEmployer([FromBody] Employers employer)
        {
            var data=_employerRepo.EditDetails(employer);
            if (data!=null)
            {
                return Ok(data);
            }

            return BadRequest();

        }

        [HttpGet, Route("getbyuserid/{id}")]
        public IHttpActionResult GetEmployerDetailsBYUID(int id)
        {
            var employer = _employerRepo.GetDetailsBYUID(id);
            if (employer != null)
            {
                return Ok(employer);
            }

            return Ok("null");
        }

        [HttpGet, Route("getbyemployerid/{id}")]
        public IHttpActionResult GetEmployerDetailsByEID(int id)
        {
            var employer = _employerRepo.GetDetailsByEID(id);
            if (employer != null)
            {
                return Ok(employer);
            }

            return Ok("null");
        }

        [HttpPost, Route("register/")]
        public IHttpActionResult RegisterEmployer(Employers employer)
        {
            var employerData = _employerRepo.Register(employer);
            if (employer != null)
            {
                return Ok(employerData);
            }

            return Ok("null");
        }

        [HttpDelete, Route("delete/")]
        public IHttpActionResult DeleteEmployer(int id)
        {
            var employerData = _employerRepo.DeleteEmployer(id);
            if (employerData != null)
            {
                return Ok(employerData);
            }

            return Ok("null");
        }

        
    }
}
