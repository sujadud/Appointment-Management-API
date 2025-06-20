using Appointment_Management.Application.DTOs;
using Appointment_Management.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Appointment_Management.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly DoctorService _doctorService;

        public DoctorsController(DoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [Authorize(Roles = "User,Doctor,Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllDoctors()
        {
            // Debug claims
            //var identity = User.Identity as ClaimsIdentity;
            //var claims = identity?.Claims.Select(c => new { c.Type, c.Value }).ToList();
            //_logger.LogInformation("User Claims: {@Claims}", claims);
            var doctors = await _doctorService.GetAllAsync();
            return Ok(doctors);
        }


        [Authorize(Roles = "User,Doctor,Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctorById(Guid id)
        {
            var doctor = await _doctorService.GetByIdAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return Ok(doctor);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateDoctor([FromBody] DoctorDto doctor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _doctorService.AddAsync(doctor);
            return CreatedAtAction(nameof(GetDoctorById), new { id = doctor.Id }, doctor);
        }

        [Authorize(Roles = "Doctor,Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDoctor(Guid id, [FromBody] DoctorDto doctor)
        {
            if (id != doctor.Id)
            {
                return BadRequest("Doctor ID mismatch.");
            }

            var existingDoctor = await _doctorService.GetByIdAsync(id);
            if (existingDoctor == null)
            {
                return NotFound();
            }

            await _doctorService.UpdateAsync(doctor);
            return Ok(doctor);
        }

        [Authorize(Roles = "Doctor,Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(Guid id)
        {
            var doctor = await _doctorService.GetByIdAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }

            await _doctorService.DeleteAsync(id);
            return Ok();
        }
    }
}
