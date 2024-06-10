using Lab11.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab11.Data;

public class ApbdContext : DbContext
{
    public ApbdContext()
    {
    }

    public ApbdContext(DbContextOptions<ApbdContext> options) : base(options)
    {
    }
    
    public DbSet<Prescription> Presciptions { get; set; }
    public DbSet<Prescription_Medicament> PrescriptionMedicaments { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Prescription>()
            .HasOne(p => p.Doctor)
            .WithMany(d => d.Presciption)
            .HasForeignKey(p => p.IdDoctor);

        modelBuilder.Entity<Prescription>()
            .HasOne(p => p.Patient)
            .WithMany(p => p.Presciption)
            .HasForeignKey(p => p.IdPatient);

        modelBuilder.Entity<Prescription_Medicament>()
            .HasOne(m => m.Medicament)
            .WithMany(p => p.PrescriptionMedicaments)
            .HasForeignKey(m => m.IdMedicament);

        modelBuilder.Entity<Prescription_Medicament>()
            .HasOne(p => p.Prescription)
            .WithMany(p => p.PrescriptionMedicaments)
            .HasForeignKey(p => p.IdPrescription);
    }
}