using Appointment_Management.Domain.Entities.Enums;
using Appointment_Management.Domain.Interfaces.IAudit;
using System.ComponentModel.DataAnnotations;

namespace Appointment_Management.Domain.Entities
{
    public class User : IEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public RoleType Role { get; set; }

        // Additional properties for auditing
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
