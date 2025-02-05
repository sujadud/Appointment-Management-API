using Xunit;
using Moq;
using FluentAssertions;
using Appointment_Management.Application.Services;
using Appointment_Management.Domain.Entities;
using Appointment_Management.Domain.Interfaces;
using Appointment_Management.Application.DTOs;

public class AppointmentTests
{
    private readonly AppointmentService _service;
    private readonly Mock<ICommonRepository<Appointment>> _mockRepo;

    public AppointmentTests()
    {
        _mockRepo = new Mock<ICommonRepository<Appointment>>();
        _service = new AppointmentService(_mockRepo.Object);
    }

    [Fact]
    public async Task CreateAppointment_WithPastDate_ShouldFail()
    {
        var appointment = new AppointmentDto
        {
            Id = Guid.NewGuid(),
            AppointmentDateTime = DateTime.UtcNow.AddDays(-1),
            DoctorId = Guid.NewGuid()
        };

        var exception = await Assert.ThrowsAsync<Exception>(() => _service.AddAsync(appointment));
        exception.Message.Should().Be("Appointment date must be in the future.");
    }

    [Fact]
    public async Task CreateAppointment_WithValidData_ShouldSucceed()
    {
        var appointment = new Appointment
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            AppointmentDateTime = DateTime.UtcNow.AddDays(2),
            DoctorId = Guid.Parse("08e90631-5fff-40cc-abe4-08dd450efc6d")
        };

        _mockRepo.Setup(repo => repo.AddAsync(appointment)).Returns(Task.CompletedTask);

        var appointmentDto = new AppointmentDto
        {
            Id = appointment.Id,
            PatientName = appointment.Name,
            AppointmentDateTime = appointment.AppointmentDateTime,
            DoctorId = appointment.DoctorId
        };
                
        Func<Task> act = async () => await _service.AddAsync(appointmentDto);

        await act.Should().NotThrowAsync();
        _mockRepo.Verify(repo => repo.AddAsync(It.IsAny<Appointment>()), Times.Once);
    }
}
