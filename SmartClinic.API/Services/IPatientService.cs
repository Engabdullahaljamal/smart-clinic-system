using SmartClinic.API.Models;

namespace SmartClinic.API.Services;

public interface IPatientService
{
    Task<List<Patient>> GetAllPatientsAsync();
    Task<Patient?> GetPatientByIdAsync(int id);
    Task<Patient> AddPatientAsync(Patient patient);
    Task<bool> DeletePatientAsync(int id);
}