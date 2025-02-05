using Appointment_Management.Application.DTOs;
using Appointment_Management.Application.Services;
using FluentValidation;

namespace Appointment_Management.Application.Validation
{
    public class AppointmentValidator : AbstractValidator<AppointmentDto>
    {
        private readonly DoctorService _doctorService;

        public AppointmentValidator(DoctorService doctorService)
        {
            _doctorService = doctorService;

            RuleFor(a => a.DoctorId)
                .NotEmpty().WithMessage("Doctor ID is required.")
                .MustAsync(async (doctorId, cancellation) => await _doctorService.ExistsAsync(doctorId))
                .WithMessage("Doctor isn't valid. Please provide a valid Doctor ID.");

            RuleFor(a => a.AppointmentDateTime)
                .GreaterThan(DateTime.Now)
                .WithMessage("Appointment date must be in the future.");
        }
    }
}
