namespace Lab11.Models.Dtos;

public class GetPatientDto
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    
    public List<GetPrescriptionDto> Prescriptions { get; set; }
}