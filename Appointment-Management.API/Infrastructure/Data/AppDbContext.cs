using Appointment_Management.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Appointment_Management.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Username)
                      .IsRequired()
                      .HasMaxLength(30);
                entity.HasIndex(u => u.Username)
                      .IsUnique(true);
                entity.Property(u => u.PasswordHash)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(u => u.Salt)
                      .IsRequired()
                      .HasMaxLength(100);
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(d => d.Id);
                entity.Property(d => d.Name)
                      .IsRequired()
                      .HasMaxLength(30);
            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Name)
                      .IsRequired()
                      .HasMaxLength(30);
                entity.Property(a => a.ContactInformation)
                      .HasMaxLength(200);
                entity.Property(a => a.AppointmentDateTime)
                      .IsRequired();
                entity.HasOne(a => a.Doctor)
                      .WithMany(d => d.Appointments)
                      .HasForeignKey(a => a.DoctorId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
