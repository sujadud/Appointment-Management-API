using Appointment_Management.Domain.Interfaces.IAudit;
using System.ComponentModel.DataAnnotations;

namespace Appointment_Management.Domain.Entities
{
    public class Doctor : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;

        // Additional properties for auditing
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<Appointment>? Appointments { get; set; }
    }
}
