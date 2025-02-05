using Appointment_Management.Application.DTOs;
using Appointment_Management.Domain.Interfaces;
using Appointment_Management.Domain.Entities;

namespace Appointment_Management.Application.Services
{
    public class AppointmentService
    {
        private readonly ICommonRepository<Appointment> _repository;

        public AppointmentService(ICommonRepository<Appointment> repository)
        {
            _repository = repository;
        }        

        public async Task SaveChangesAsync()
        {
            await _repository.SaveAsync();
        }

        public async Task<AppointmentDto> GetByIdAsync(Guid id)
        {
            var appointment = await _repository.GetByIdAsync(id);
            return new AppointmentDto 
            {
                Id = appointment.Id,
                PatientName = appointment.Name,
                PatientContactInformation = appointment.ContactInformation,
                AppointmentDateTime = appointment.AppointmentDateTime,
                DoctorId = appointment.DoctorId
            };
        }

        public async Task<IEnumerable<AppointmentDto>> GetAllAsync()
        {
            var appointments = await _repository.GetAllAsync();
            return appointments.Select(appointment => new AppointmentDto
            {
                Id = appointment.Id,
                PatientName = appointment.Name,
                PatientContactInformation = appointment.ContactInformation,
                AppointmentDateTime = appointment.AppointmentDateTime,
                DoctorId = appointment.DoctorId
            });
        }

        public async Task AddAsync(AppointmentDto appointment)
        {
            var newAppointment = new Appointment
            {
                Id = appointment.Id,
                Name = appointment.PatientName,
                ContactInformation = appointment.PatientContactInformation,
                AppointmentDateTime = appointment.AppointmentDateTime,
                DoctorId = appointment.DoctorId
            };
            await _repository.AddAsync(newAppointment);
            await _repository.SaveAsync();
        }

        public async Task UpdateAsync(AppointmentDto appointment)
        {
            var existAppointment = await _repository.GetByIdAsync(appointment.Id);
            if (existAppointment != null)
            {
                existAppointment.Name = appointment.PatientName;
                existAppointment.ContactInformation = appointment.PatientContactInformation;
                existAppointment.AppointmentDateTime = appointment.AppointmentDateTime;
                existAppointment.DoctorId = appointment.DoctorId;
                await _repository.UpdateAsync(existAppointment);
            }
            await _repository.SaveAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
            await _repository.SaveAsync();
        }
    }
}
