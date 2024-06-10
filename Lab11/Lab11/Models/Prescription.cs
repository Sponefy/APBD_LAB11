using System.ComponentModel.DataAnnotations;

namespace Lab11.Models;

public class Prescription
{
    [Key]
    public int IdPrescription { get; set; }
    public DateOnly Date { get; set; }
    public DateOnly DueDate { get; set; }
    public int IdPatient { get; set; }
    public int IdDoctor { get; set; }
    
    
    public Patient Patient { get; set; }
    public Doctor Doctor { get; set; }
    public ICollection<Prescription_Medicament> PrescriptionMedicaments { get; set; }
}