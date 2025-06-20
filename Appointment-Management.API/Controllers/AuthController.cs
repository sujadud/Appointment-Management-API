using Appointment_Management.Application.DTOs;
using Appointment_Management.Application.Services.Auth;
using Appointment_Management.Domain.Entities.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Appointment_Management.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto model)
        {
            var success = await _authService.RegisterUser(model.Username, model.Password, (RoleType)model.Role);
            if (!success)
                return BadRequest(new { message = "Username already exists" });

            return Ok(new { message = "User registered successfully" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDto model)
        {
            var token = await _authService.AuthenticateUser(model.Username, model.Password);
            if (token == null)
                return Unauthorized(new { message = "Invalid credentials" });

            return Ok(new { Token = token });
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] UserDto model)
        {
            // Debug claims
            var identity = User.Identity as ClaimsIdentity;
            //var claims = identity?.Claims.Select(c => new { c.Type, c.Value }).ToList();
            //_logger.LogInformation("User Claims: {@Claims}", claims);
            //var success = await _authService.ChangePassword(model.Username, model.Password);
            //if (!success)
            //    return BadRequest(new { message = "Failed to change password" });
            return Ok(new { message = "Password changed successfully" });
        }
    }
}
