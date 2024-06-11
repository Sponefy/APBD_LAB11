using System.Text.Json.Serialization;

namespace Lab11.Models.Dtos;

public class GetMedicamentDto
{   
    public int IdMedicament { get; set; }
    public string Name { get; set; }
    public int Dose { get; set; }
    public string Description { get; set; }
}