using Lab11.Data;
using Lab11.Models;
using Lab11.Models.Dtos;
using Lab11.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab11.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrescriptionController : ControllerBase
{
    private readonly IMedicalService _medicalService;

    public PrescriptionController(IMedicalService medicalService)
    {
        _medicalService = medicalService;
    }

    [HttpPost]
    public async Task<IActionResult> PostPrescription(PrescriptionDto prescription)
    {
        var error = await _medicalService.ValidatePrescription(prescription);

        if (error != "Ok")
        {
            return BadRequest(error);
        }

        var id = await _medicalService.SavePrescription(prescription);

        return Ok(new {Id = id});
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatient(int id)
    {
        var patient = await _medicalService.GetPatient(id);

        if (patient == null)
        {
            return NotFound();
        }

        return Ok(patient);
    }
}