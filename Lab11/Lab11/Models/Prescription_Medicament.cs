using System.ComponentModel.DataAnnotations;

namespace Lab11.Models;

public class Prescription_Medicament
{
    public int IdMedicament { get; set; }
    public int IdPrescription { get; set; }
    public int Dose { get; set; }
    public string Details { get; set; }
    
    
    public Medicament Medicament { get; set; }
    public Prescription Prescription { get; set; }
}