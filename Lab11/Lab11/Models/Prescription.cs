using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab11.Models;

public class Prescription
{
    [Key]
    public int IdPrescription { get; set; }
    [Column(TypeName = "date")]
    public DateTime Date { get; set; }
    [Column(TypeName = "date")]
    public DateTime DueDate { get; set; }
    public int IdPatient { get; set; }
    public int IdDoctor { get; set; }
    
    
    public Patient Patient { get; set; }
    
    // ForeginKey zamiast dodwania kodu do context (fluent)
    [ForeignKey(nameof(IdDoctor))]
    public Doctor Doctor { get; set; }
    public ICollection<Prescription_Medicament> PrescriptionMedicaments { get; set; }
}