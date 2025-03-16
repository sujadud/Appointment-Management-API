using Appointment_Management.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace Appointment_Management.Domain.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Salt { get; set; } = string.Empty;
        public RoleType Role { get; set; }
    }
}
