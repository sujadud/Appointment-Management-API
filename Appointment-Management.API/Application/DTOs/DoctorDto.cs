﻿using System.ComponentModel.DataAnnotations;

namespace Appointment_Management.Application.DTOs
{
    public class DoctorDto
    {
        public Guid Id { get; set; }
        [Required]
        public string DoctorName { get; set; } = string.Empty;

        public ICollection<AppointmentDto?> Appointments { get; set; } = new List<AppointmentDto?>();
    }
}
