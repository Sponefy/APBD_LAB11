using Lab11.Data;
using Lab11.Models.Dtos;
using Lab11.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab11.Services;

public class MedicalService : IMedicalService
{
    private readonly ApbdContext _context;

    public MedicalService(ApbdContext context)
    {
        _context = context;
    }

    public async Task<string> ValidatePrescription(PrescriptionDto prescription)
    {
        // Sprawdzanie, czy doktor istnieje
        if (!await _context.Doctors.AnyAsync(x => x.IdDoctor == prescription.DoctorId))
        {
            return "Doctor doesn't exist";
        }

        // Inny sposób ale mniej optymalny:

        // var doctor = await _context.Doctors.FindAsync(prescription.DoctorId);
        // if (doctor == null)
        // {
        //     return BadRequest("Doctor doesn't exist");
        // }

        // Sprawdzanie, czy lek istnieje i czy ilość jest zgodna
        if (prescription.Medicaments.Count > 10)
        {
            return "Prescriptions can have a maximum of 10 medicaments";
        }

        foreach (var medicamentDto in prescription.Medicaments)
        {
            var medicament = await _context.Medicaments.FindAsync(medicamentDto.IdMedicament);

            if (medicament == null)
            {
                return $"Medicament with ID {medicamentDto.IdMedicament} does not exist";
            }
        }

        // Ignorowanie zbędnych danych z dat (sekundy itp.)
        if (prescription.DueDate != prescription.DueDate.Date)
        {
            return "DueDate invalid";
        }

        if (prescription.Date != prescription.Date.Date)
        {
            return "Date invalid";
        }

        // Sprawdzanie, czy daty ważności się zgadza
        if (prescription.DueDate < prescription.Date)
        {
            return "DueDate must be greater than or equal to Date";
        }

        return "Ok";
    }

    public async Task<int> SavePrescription(PrescriptionDto prescription)
    {
        // Tworzenie transakcji
        // await using var transaction = await _context.Database.BeginTransactionAsync();

        // Sprawdzanie, czy pacjent istnieje
        var patient = await _context.Patients.FindAsync(prescription.Patient.IdPatient);
        if (patient == null)
        {
            patient = new Patient
            {
                FirstName = prescription.Patient.FirstName,
                LastName = prescription.Patient.LastName,
                BirthDate = prescription.Patient.BirthDate
            };
            await _context.Patients.AddAsync(patient);
            // await _context.SaveChangesAsync();
        }

        // Tworzenie nowej recepty
        var newPrescription = new Prescription
        {
            Date = prescription.Date,
            DueDate = prescription.DueDate,
            //IdPatient = patient.IdPatient,
            Patient = patient,
            IdDoctor = prescription.DoctorId
        };
        await _context.Presciptions.AddAsync(newPrescription);

        // Dodawanie leków do recepty
        foreach (var medicament in prescription.Medicaments)
        {
            var prescriptionMedicament = new Prescription_Medicament
            {
                // IdPrescription = newPrescription.IdPrescription,
                Prescription = newPrescription,
                IdMedicament = medicament.IdMedicament,
                Dose = medicament.Dose,
                Details = medicament.Description
            };
            _context.PrescriptionMedicaments.Add(prescriptionMedicament);
        }

        await _context.SaveChangesAsync();
        // await transaction.CommitAsync();

        return newPrescription.IdPrescription;
    }

    public async Task<GetPatientDto?> GetPatient(int id)
    {
        var patient = await _context.Patients
            .Where(x => x.IdPatient == id)
            .Select(x => new GetPatientDto
            {
                IdPatient = x.IdPatient,
                FirstName = x.FirstName,
                LastName = x.LastName,
                BirthDate = x.BirthDate
            })
            .SingleOrDefaultAsync();
        
        if (patient == null)
        {
            return null;
        }

        patient.Prescriptions = await _context.Presciptions
            .Where(x => x.IdPatient == id)
            .Select(x => new GetPrescriptionDto
            {
                IdPrescription = x.IdPrescription,
                Date = x.Date,
                DueDate = x.DueDate,
                Doctor = new GetDoctorDto
                {
                    IdDoctor = x.IdDoctor,
                    FirstName = x.Doctor.FirstName
                }
            })
            .ToListAsync();

        foreach (var prescription in patient.Prescriptions)
        {
            prescription.Medicaments = await _context.PrescriptionMedicaments
                .Where(x => x.IdPrescription == prescription.IdPrescription)
                .Select(x => new GetMedicamentDto
                {
                    IdMedicament = x.IdMedicament,
                    Name = x.Medicament.Name,
                    Description = x.Details,
                    Dose = x.Dose
                })
                .ToListAsync();
        }
        
        return patient;
    }
}