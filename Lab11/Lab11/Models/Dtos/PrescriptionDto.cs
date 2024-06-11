using System.Text.Json.Serialization;

namespace Lab11.Models.Dtos;

public class PrescriptionDto
{
    // Zmiana nazwy dotyczy tylko json
    [JsonPropertyName("patient")]
    public PatientDto Patient { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public int DoctorId { get; set; }
    [JsonPropertyName("medicaments")]
    public List<MedicamentDto> Medicaments { get; set; }
}

