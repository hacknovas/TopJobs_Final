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
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private UserRepository _userRepository;
        public UserController()
        {
            _userRepository = new UserRepository();
        }

        [HttpPost, Route("login/")]
        public IHttpActionResult Login([FromBody] LoginRequest loginRequest)
        {
            var user = _userRepository.Login(loginRequest.Username, loginRequest.Password);
            if (user != null)
            {
                return Ok(user);
            }
            return Ok("Login Failed");
        }

        [HttpPost, Route("register")]
        public IHttpActionResult Register([FromBody] Users user)
        {
            var userData = _userRepository.Register(user);
                return Ok(userData);
        }
    }
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
