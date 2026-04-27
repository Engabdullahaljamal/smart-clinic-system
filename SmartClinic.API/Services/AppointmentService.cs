using Microsoft.EntityFrameworkCore;
using SmartClinic.API.Data;
using SmartClinic.API.Models;

namespace SmartClinic.API.Services;

public class AppointmentService : IAppointmentService
{
    private readonly SmartClinicDbContext _context;

    public AppointmentService(SmartClinicDbContext context)
    {
        _context = context;
    }

    public async Task<List<Appointment>> GetAllAppointmentsAsync()
    {
        return await _context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .ToListAsync();
    }

    public async Task<Appointment?> GetAppointmentByIdAsync(int id)
    {
        return await _context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .FirstOrDefaultAsync(a => a.AppointmentId == id);
    }

    public async Task<Appointment> AddAppointmentAsync(Appointment appointment)
    {
        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();
        return appointment;
    }
    public async Task<bool> UpdateAppointmentAsync(int id, Appointment updated)
    {
        var appointment = await _context.Appointments.FindAsync(id);

        if (appointment == null)
            return false;

        appointment.AppointmentDate = updated.AppointmentDate;
        appointment.Status = updated.Status;
        appointment.Notes = updated.Notes;
        appointment.PatientId = updated.PatientId;
        appointment.DoctorId = updated.DoctorId;

        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> DeleteAppointmentAsync(int id)
    {
        var appointment = await _context.Appointments.FindAsync(id);

        if (appointment == null)
        {
            return false;
        }

        _context.Appointments.Remove(appointment);
        await _context.SaveChangesAsync();

        return true;
    }
}