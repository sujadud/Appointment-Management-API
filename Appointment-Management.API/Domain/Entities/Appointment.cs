using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Appointment_Management.Domain.Interfaces;

namespace Appointment_Management.Domain.Entities
{
    public class Appointment : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ContactInformation { get; set; } = string.Empty;
        public DateTime AppointmentDateTime { get; set; }

        public Guid DoctorId { get; set; }

        [ForeignKey("DoctorId")]
        public Doctor Doctor { get; set; }
    }
}