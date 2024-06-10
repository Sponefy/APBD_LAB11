using System.ComponentModel.DataAnnotations;

namespace Lab11.Models;

public class Doctor
{
    [Key]
    public int IdDoctor { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    
    
    public ICollection<Prescription> Presciption { get; set; }
}