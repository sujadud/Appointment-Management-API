using Appointment_Management.Application.DTOs;
using Appointment_Management.Application.Services;
using Appointment_Management.Application.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Appointment_Management.API.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly AppointmentService _appointmentService;
        private readonly DoctorService _doctorService;
        private readonly AppointmentValidator _validator;

        public AppointmentsController(AppointmentService appointmentService, 
                                        AppointmentValidator appointmentValidator, 
                                        DoctorService doctorService)
        {
            _appointmentService = appointmentService;
            _validator = appointmentValidator;
            _doctorService = doctorService;
        }

        // GET: api/appointments
        [Authorize(Roles = "Doctor,Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllAppointments()
        {
            var appointments = await _appointmentService.GetAllAsync();
            return Ok(appointments);
        }

        // GET: api/appointments/{id}
        [Authorize(Roles = "User,Doctor,Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointmentById(Guid id)
        {
            var appointment = await _appointmentService.GetByIdAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return Ok(appointment);
        }

        // POST: api/appointments
        [Authorize(Roles = "User,Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateAppointment([FromBody] AppointmentDto appointment)
        {
            var validationResult = await _validator.ValidateAsync(appointment);
            var dateTimeNow = DateTime.UtcNow;
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            //bool isDoctorExist = await _doctorService.ExistsAsync(appointment.DoctorId);
            //if (isDoctorExist)
            //{
            //    var doctor = await _doctorService.GetByIdAsync(appointment.DoctorId);
            //    doctor.Appointments.Add(new AppointmentDto
            //    {
            //        Id = appointment.Id,
            //        PatientName = appointment.PatientName,
            //        PatientContactInformation = appointment.PatientContactInformation,
            //        AppointmentDateTime = appointment.AppointmentDateTime,
            //        DoctorId = appointment.DoctorId
            //    });
            //    _doctorService.UpdateAsync(doctor);
            //}
            //await _appointmentService.SaveChangesAsync();

            await _appointmentService.AddAsync(appointment);

            return CreatedAtAction(nameof(GetAppointmentById), new { id = appointment.Id }, appointment);
        }

        // PUT: api/appointments/{id}
        [Authorize(Roles = "User,Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(Guid id, [FromBody] AppointmentDto appointment)
        {
            if (id != appointment.Id)
            {
                return BadRequest("Appointment ID mismatch.");
            }

            var existingAppointment = await _appointmentService.GetByIdAsync(id);
            if (existingAppointment == null)
            {
                return NotFound();
            }

            await _appointmentService.UpdateAsync(appointment);
            return NoContent();
        }

        // DELETE: api/appointments/{id}
        [Authorize(Roles = "User,Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(Guid id)
        {
            var appointment = await _appointmentService.GetByIdAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            await _appointmentService.DeleteAsync(id);
            return NoContent();
        }
    }
}
