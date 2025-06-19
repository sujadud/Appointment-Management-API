using Appointment_Management.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace Appointment_Management.Application.DTOs
{
    public class UserDto
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public RoleType Role { get; set; } = RoleType.User;
    }
}