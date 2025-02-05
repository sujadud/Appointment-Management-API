using Appointment_Management.Application.DTOs;
using Appointment_Management.Domain.Interfaces;
using Appointment_Management.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Appointment_Management.Application.Services
{
    public class DoctorService
    {
        private readonly ICommonRepository<Doctor> _repository;

        public DoctorService(ICommonRepository<Doctor> repository)
        {
            _repository = repository;
        }

        public async Task<bool> ExistsAsync(Guid doctorId)
        {
            return await _repository.ExistsAsync(doctorId);
        }

        public async Task<DoctorDto> GetByIdAsync(Guid id)
        {
            var doctor = await _repository.GetByIdAsync(id);
            return new DoctorDto
            {
                Id = doctor.Id,
                DoctorName = doctor.Name,
            };
        }

        public async Task<IEnumerable<DoctorDto>> GetAllAsync()
        {
            var doctors = await _repository.GetAllAsync();
            return doctors.Select(d => new DoctorDto
            {
                Id = d.Id,
                DoctorName = d.Name,
            });
        }

        public async Task AddAsync(DoctorDto doctor)
        {            
            await _repository.AddAsync(new Doctor
            {
                Name = doctor.DoctorName,
            });
            await _repository.SaveAsync();
        }

        public async Task UpdateAsync(DoctorDto doctor)
        {
            var existingDoctor = await _repository.GetByIdAsync(doctor.Id);
            if (existingDoctor != null)
            {
                existingDoctor.Name = doctor.DoctorName;
                existingDoctor.Appointments = doctor.Appointments.Select(a => new Appointment
                {
                    Id = a.Id,
                    Name = a.PatientName,
                    ContactInformation = a.PatientContactInformation,
                    AppointmentDateTime = a.AppointmentDateTime,
                    DoctorId = a.DoctorId
                }).ToList();
                await _repository.UpdateAsync(existingDoctor);
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
