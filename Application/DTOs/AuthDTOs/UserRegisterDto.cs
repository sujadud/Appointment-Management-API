using Appointment_Management.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.AuthDTOs
{
    public class UserRegisterDto
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public int Role { get; set; } = (int)RoleType.User;
    }
}