using System.ComponentModel.DataAnnotations;

namespace Lab11.Models;

public class Patient
{
    [Key]
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly BirthDate { get; set; }
    
    
    public ICollection<Prescription> Presciption { get; set; }
}