using Microsoft.EntityFrameworkCore;
using SmartClinic.API.Data;
using SmartClinic.API.Models;

namespace SmartClinic.API.Services;

public class PatientService : IPatientService
{
    private readonly SmartClinicDbContext _context;

    public PatientService(SmartClinicDbContext context)
    {
        _context = context;
    }

    public async Task<List<Patient>> GetAllPatientsAsync()
    {
        return await _context.Patients.ToListAsync();
    }

    public async Task<Patient?> GetPatientByIdAsync(int id)
    {
        return await _context.Patients.FindAsync(id);
    }

    public async Task<Patient> AddPatientAsync(Patient patient)
    {
        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();
        return patient;
    }
    public async Task<bool> UpdatePatientAsync(int id, Patient updatedPatient)
    {
        var patient = await _context.Patients.FindAsync(id);

        if (patient == null)
            return false;

        patient.FullName = updatedPatient.FullName;
        patient.DateOfBirth = updatedPatient.DateOfBirth;
        patient.PhoneNumber = updatedPatient.PhoneNumber;
        patient.Email = updatedPatient.Email;
        patient.Address = updatedPatient.Address;

        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> DeletePatientAsync(int id)
    {
        var patient = await _context.Patients.FindAsync(id);

        if (patient == null)
        {
            return false;
        }

        _context.Patients.Remove(patient);
        await _context.SaveChangesAsync();

        return true;
    }
}