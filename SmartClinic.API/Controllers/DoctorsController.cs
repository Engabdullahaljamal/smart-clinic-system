using Microsoft.AspNetCore.Mvc;
using SmartClinic.API.DTO;
using SmartClinic.API.Models;
using SmartClinic.API.Services;

namespace SmartClinic.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DoctorsController : ControllerBase
{
    private readonly IDoctorService _doctorService;

    public DoctorsController(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    [HttpGet]
    public async Task<ActionResult<List<DoctorResponseDTO>>> GetDoctors()
    {
        var doctors = await _doctorService.GetAllDoctorsAsync();

        var result = doctors.Select(d => new DoctorResponseDTO
        {
            DoctorId = d.DoctorId,
            FullName = d.FullName,
            Email = d.Email,
            PhoneNumber = d.PhoneNumber,
            DepartmentId = d.DepartmentId,
            DepartmentName = d.Department?.Name
        }).ToList();

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DoctorResponseDTO>> GetDoctorById(int id)
    {
        var doctor = await _doctorService.GetDoctorByIdAsync(id);

        if (doctor == null)
        {
            return NotFound();
        }

        var result = new DoctorResponseDTO
        {
            DoctorId = doctor.DoctorId,
            FullName = doctor.FullName,
            Email = doctor.Email,
            PhoneNumber = doctor.PhoneNumber,
            DepartmentId = doctor.DepartmentId,
            DepartmentName = doctor.Department?.Name
        };

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<DoctorResponseDTO>> AddDoctor(DoctorCreateDTO dto)
    {
        var doctor = new Doctor
        {
            FullName = dto.FullName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            DepartmentId = dto.DepartmentId
        };

        var createdDoctor = await _doctorService.AddDoctorAsync(doctor);

        var result = new DoctorResponseDTO
        {
            DoctorId = createdDoctor.DoctorId,
            FullName = createdDoctor.FullName,
            Email = createdDoctor.Email,
            PhoneNumber = createdDoctor.PhoneNumber,
            DepartmentId = createdDoctor.DepartmentId,
            DepartmentName = createdDoctor.Department?.Name
        };

        return CreatedAtAction(
            nameof(GetDoctorById),
            new { id = result.DoctorId },
            result
        );
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDoctor(int id, DoctorCreateDTO dto)
    {
        var doctor = new Doctor
        {
            FullName = dto.FullName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            DepartmentId = dto.DepartmentId
        };

        var updated = await _doctorService.UpdateDoctorAsync(id, doctor);

        if (!updated)
            return NotFound();

        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDoctor(int id)
    {
        var deleted = await _doctorService.DeleteDoctorAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}