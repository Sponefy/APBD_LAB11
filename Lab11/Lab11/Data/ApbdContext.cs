using Lab11.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Lab11.Data;

public class ApbdContext : DbContext
{
    private readonly IConfiguration _configuration;

    // public ApbdContext(IConfiguration configuration)
    // {
    //     _configuration = configuration;
    // }

    public ApbdContext(DbContextOptions<ApbdContext> options) : base(options)
    {
    }
    
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseSqlServer(_configuration.GetConnectionString("Default"));
    // }
    
    public DbSet<Prescription> Presciptions { get; set; }
    public DbSet<Prescription_Medicament> PrescriptionMedicaments { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Prescription_Medicament>()
            .HasKey(pm => new { pm.IdMedicament, pm.IdPrescription });
        
        // modelBuilder.Entity<Prescription>()
        //     .HasOne(p => p.Doctor)
        //     .WithMany(d => d.Presciption)
        //     .HasForeignKey(p => p.IdDoctor);

        modelBuilder.Entity<Prescription>()
            .HasOne(p => p.Patient)
            .WithMany(p => p.Presciptions)
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