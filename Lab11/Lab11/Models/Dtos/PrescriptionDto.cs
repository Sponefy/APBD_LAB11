namespace Lab11.Models.Dtos;

public class PrescriptionDto
{
    public DateOnly Date { get; set; }
    public DateOnly DueDate { get; set; }
    public int DoctorId { get; set; }
    public int PatientId { get; set; }
    public string PatientFirstName { get; set; }
    public string PatientLastName { get; set; }
    public DateOnly PatientBirthDate { get; set; }
    public List<int> MedicamentIds { get; set; }
}