using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab11.Models;

public class Doctor
{
    [Key]
    public int IdDoctor { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    
    //[InverseProperty(nameof(Prescription.Doctor))]
    public ICollection<Prescription> Presciptions { get; set; }
}