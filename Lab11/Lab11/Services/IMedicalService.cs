using Lab11.Models.Dtos;

namespace Lab11.Services;

public interface IMedicalService
{
    Task<string> ValidatePrescription(PrescriptionDto prescription);
    Task<int> SavePrescription(PrescriptionDto prescription);
}