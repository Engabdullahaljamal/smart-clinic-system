using Microsoft.EntityFrameworkCore;
using SmartClinic.API.Data;
using SmartClinic.API.Models;

namespace SmartClinic.API.Services;

public class DoctorService : IDoctorService
{
    private readonly SmartClinicDbContext _context;

    public DoctorService(SmartClinicDbContext context)
    {
        _context = context;
    }

    public async Task<List<Doctor>> GetAllDoctorsAsync()
    {
        return await _context.Doctors
            .Include(d => d.Department)
            .ToListAsync();
    }

    public async Task<Doctor?> GetDoctorByIdAsync(int id)
    {
        return await _context.Doctors
            .Include(d => d.Department)
            .FirstOrDefaultAsync(d => d.DoctorId == id);
    }

    public async Task<Doctor> AddDoctorAsync(Doctor doctor)
    {
        _context.Doctors.Add(doctor);
        await _context.SaveChangesAsync();
        return doctor;
    }
    public async Task<bool> UpdateDoctorAsync(int id, Doctor updatedDoctor)
    {
        var doctor = await _context.Doctors.FindAsync(id);

        if (doctor == null)
            return false;

        doctor.FullName = updatedDoctor.FullName;
        doctor.Email = updatedDoctor.Email;
        doctor.PhoneNumber = updatedDoctor.PhoneNumber;
        doctor.DepartmentId = updatedDoctor.DepartmentId;

        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> DeleteDoctorAsync(int id)
    {
        var doctor = await _context.Doctors.FindAsync(id);

        if (doctor == null)
        {
            return false;
        }

        _context.Doctors.Remove(doctor);
        await _context.SaveChangesAsync();

        return true;
    }
}