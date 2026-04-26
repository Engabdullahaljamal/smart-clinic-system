using Microsoft.EntityFrameworkCore;
using SmartClinic.API.Models;

namespace SmartClinic.API.Data;

public class SmartClinicDbContext : DbContext
{
    public SmartClinicDbContext(DbContextOptions<SmartClinicDbContext> options)
        : base(options)
    {
    }

    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Specialization> Specializations { get; set; }
    public DbSet<DoctorSpecialization> DoctorSpecializations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<DoctorSpecialization>()
            .HasKey(ds => new { ds.DoctorId, ds.SpecializationId });

        modelBuilder.Entity<DoctorSpecialization>()
            .HasOne(ds => ds.Doctor)
            .WithMany(d => d.DoctorSpecializations)
            .HasForeignKey(ds => ds.DoctorId);

        modelBuilder.Entity<DoctorSpecialization>()
            .HasOne(ds => ds.Specialization)
            .WithMany(s => s.DoctorSpecializations)
            .HasForeignKey(ds => ds.SpecializationId);
    }
}