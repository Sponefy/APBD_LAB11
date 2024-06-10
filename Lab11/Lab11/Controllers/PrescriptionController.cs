using Lab11.Data;
using Lab11.Models;
using Lab11.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Lab11.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrescriptionController : ControllerBase
{
    private readonly ApbdContext _context;

    public PrescriptionController(ApbdContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> PostPrescription(PrescriptionDto prescription)
    {
        // Sprawdzanie, czy pacjent istnieje
        var patient = await _context.Patients.FindAsync(prescription.PatientId);
        if (patient == null)
        {
            patient = new Patient
            {
                FirstName = prescription.PatientFirstName,
                LastName = prescription.PatientLastName,
                BirthDate = prescription.PatientBirthDate

            };
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }

        // Sprawdzanie, czy doktor istnieje
        var doctor = await _context.Doctors.FindAsync(prescription.DoctorId);
        if (doctor == null)
        {
            return BadRequest("Doctor doesn't exist");
        }
        
        // Sprawdzanie, czy lek istnieje i czy ilość jest zgodna
        if (prescription.MedicamentIds.Count > 10)
        {
            return BadRequest("Prescriptions can have a maximum of 10 medicaments");
        }

        foreach (var medicamentId in prescription.MedicamentIds)
        {
            var medicament = await _context.Medicaments.FindAsync(medicamentId);

            if (medicament == null)
            {
                return BadRequest($"Medicament with ID {medicamentId} does not exist");
            }
        }
        
        // Sprawdzanie, czy daty ważności się zgadza
        if (prescription.DueDate < prescription.Date)
        {
            return BadRequest("DueDate must be greater than or equal to Date");
        }
        
        // Tworzenie nowej recepty
        var newPrescription = new Prescription
        {
            Date = prescription.Date,
            DueDate = prescription.DueDate,
            IdPatient = patient.IdPatient,
            IdDoctor = doctor.IdDoctor
        };
        
        // Dodawanie leków do recepty
        foreach (var medicamentId in prescription.MedicamentIds)
        {
            var prescriptionMedicament = new Prescription_Medicament
            {
                IdPrescription = newPrescription.IdPrescription,
                IdMedicament = medicamentId
            };
            _context.PrescriptionMedicaments.Add(prescriptionMedicament);
        }

        await _context.SaveChangesAsync();

        return CreatedAtAction("GetPrescription", new { id = newPrescription.IdPrescription }, prescription);
    }
}