namespace Lab11.Models;

public class Patient
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly BirthDate { get; set; }
    
    
    public ICollection<Prescription> Presciption { get; set; }
}