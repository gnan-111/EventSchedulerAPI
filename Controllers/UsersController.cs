using EventSchedulerAPI.Models;
using EventSchedulerAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EventScheduler.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        // Dummy methods for structure; implement authentication in production
        [HttpPost("register")]
        public IActionResult Register(RegisterDto registerDto)
        {
            // Registration logic here
            return Ok(new { message = "User registered (stub)" });
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto loginDto)
        {
            // Login logic here
            return Ok(new { token = "dummy-jwt-token" });
        }
    }
}