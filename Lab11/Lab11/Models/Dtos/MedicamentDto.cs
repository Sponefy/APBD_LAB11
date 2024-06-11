using System.Text.Json.Serialization;

namespace Lab11.Models.Dtos;

public class MedicamentDto
{   
    [JsonPropertyName("idMedicament")]
    public int IdMedicament { get; set; }
    public int Dose { get; set; }
    public string Description { get; set; }
}