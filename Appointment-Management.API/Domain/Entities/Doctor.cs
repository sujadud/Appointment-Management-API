using Appointment_Management.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Appointment_Management.Domain.Entities
{
    public class Doctor : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;

        public ICollection<Appointment>? Appointments { get; set; }
    }
}
