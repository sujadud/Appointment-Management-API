namespace Appointment_Management.Application.DTOs
{
    public class AppointmentDto
    {
        public Guid Id { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public string PatientContactInformation { get; set; } = string.Empty;
        public DateTime AppointmentDateTime { get; set; }
        public Guid DoctorId { get; set; }
    }
}
