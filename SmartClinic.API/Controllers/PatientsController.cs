using Microsoft.AspNetCore.Mvc;
using SmartClinic.API.DTO;
using SmartClinic.API.Models;
using SmartClinic.API.Services;

namespace SmartClinic.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientsController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientsController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet]
    public async Task<ActionResult<List<PatientResponseDTO>>> GetPatients()
    {
        var patients = await _patientService.GetAllPatientsAsync();

        var result = patients.Select(p => new PatientResponseDTO
        {
            PatientId = p.PatientId,
            FullName = p.FullName,
            DateOfBirth = p.DateOfBirth,
            PhoneNumber = p.PhoneNumber,
            Email = p.Email,
            Address = p.Address
        }).ToList();

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PatientResponseDTO>> GetPatientById(int id)
    {
        var patient = await _patientService.GetPatientByIdAsync(id);

        if (patient == null)
        {
            return NotFound();
        }

        var result = new PatientResponseDTO
        {
            PatientId = patient.PatientId,
            FullName = patient.FullName,
            DateOfBirth = patient.DateOfBirth,
            PhoneNumber = patient.PhoneNumber,
            Email = patient.Email,
            Address = patient.Address
        };

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<PatientResponseDTO>> AddPatient(PatientCreateDTO dto)
    {
        var patient = new Patient
        {
            FullName = dto.FullName,
            DateOfBirth = dto.DateOfBirth,
            PhoneNumber = dto.PhoneNumber,
            Email = dto.Email,
            Address = dto.Address
        };

        var createdPatient = await _patientService.AddPatientAsync(patient);

        var result = new PatientResponseDTO
        {
            PatientId = createdPatient.PatientId,
            FullName = createdPatient.FullName,
            DateOfBirth = createdPatient.DateOfBirth,
            PhoneNumber = createdPatient.PhoneNumber,
            Email = createdPatient.Email,
            Address = createdPatient.Address
        };

        return CreatedAtAction(
            nameof(GetPatientById),
            new { id = result.PatientId },
            result
        );
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePatient(int id, PatientCreateDTO dto)
    {
        var patient = new Patient
        {
            FullName = dto.FullName,
            DateOfBirth = dto.DateOfBirth,
            PhoneNumber = dto.PhoneNumber,
            Email = dto.Email,
            Address = dto.Address
        };

        var updated = await _patientService.UpdatePatientAsync(id, patient);

        if (!updated)
            return NotFound();

        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePatient(int id)
    {
        var deleted = await _patientService.DeletePatientAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}