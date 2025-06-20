namespace Appointment_Management.Domain.Interfaces.IAudit;
public interface IEntity
{
    public Guid Id { get; set; }

    // Additional properties for auditing
    public Guid CreatedBy { get; set; }
    public Guid UpdatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
