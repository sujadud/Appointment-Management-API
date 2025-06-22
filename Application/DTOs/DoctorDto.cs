using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class DoctorDTO    {
        public Guid Id { get; set; }
        [Required]
        public string DoctorName { get; set; } = string.Empty;

        public ICollection<AppointmentDTO?> Appointments { get; set; } = new List<AppointmentDTO?>();
    }
}
