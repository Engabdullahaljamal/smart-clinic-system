using Microsoft.AspNetCore.Mvc;
using SmartClinic.API.DTO;
using SmartClinic.API.Models;
using SmartClinic.API.Services;

namespace SmartClinic.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;

    public AppointmentsController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    [HttpGet]
    public async Task<ActionResult<List<AppointmentResponseDTO>>> GetAppointments()
    {
        var appointments = await _appointmentService.GetAllAppointmentsAsync();

        var result = appointments.Select(a => new AppointmentResponseDTO
        {
            AppointmentId = a.AppointmentId,
            AppointmentDate = a.AppointmentDate,
            Status = a.Status,
            Notes = a.Notes,
            PatientId = a.PatientId,
            PatientName = a.Patient?.FullName,
            DoctorId = a.DoctorId,
            DoctorName = a.Doctor?.FullName
        }).ToList();

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AppointmentResponseDTO>> GetAppointmentById(int id)
    {
        var appointment = await _appointmentService.GetAppointmentByIdAsync(id);

        if (appointment == null)
        {
            return NotFound();
        }

        var result = new AppointmentResponseDTO
        {
            AppointmentId = appointment.AppointmentId,
            AppointmentDate = appointment.AppointmentDate,
            Status = appointment.Status,
            Notes = appointment.Notes,
            PatientId = appointment.PatientId,
            PatientName = appointment.Patient?.FullName,
            DoctorId = appointment.DoctorId,
            DoctorName = appointment.Doctor?.FullName
        };

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<AppointmentResponseDTO>> AddAppointment(AppointmentCreateDTO dto)
    {
        var appointment = new Appointment
        {
            AppointmentDate = dto.AppointmentDate,
            Status = dto.Status,
            Notes = dto.Notes,
            PatientId = dto.PatientId,
            DoctorId = dto.DoctorId
        };

        var createdAppointment = await _appointmentService.AddAppointmentAsync(appointment);

        var result = new AppointmentResponseDTO
        {
            AppointmentId = createdAppointment.AppointmentId,
            AppointmentDate = createdAppointment.AppointmentDate,
            Status = createdAppointment.Status,
            Notes = createdAppointment.Notes,
            PatientId = createdAppointment.PatientId,
            DoctorId = createdAppointment.DoctorId
        };

        return CreatedAtAction(
            nameof(GetAppointmentById),
            new { id = result.AppointmentId },
            result
        );
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAppointment(int id, AppointmentCreateDTO dto)
    {
        var appointment = new Appointment
        {
            AppointmentDate = dto.AppointmentDate,
            Status = dto.Status,
            Notes = dto.Notes,
            PatientId = dto.PatientId,
            DoctorId = dto.DoctorId
        };

        var updated = await _appointmentService.UpdateAppointmentAsync(id, appointment);

        if (!updated)
            return NotFound();

        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAppointment(int id)
    {
        var deleted = await _appointmentService.DeleteAppointmentAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}