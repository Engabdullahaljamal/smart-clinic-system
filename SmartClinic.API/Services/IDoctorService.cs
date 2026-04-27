using SmartClinic.API.Models;

namespace SmartClinic.API.Services;

public interface IDoctorService
{
    Task<List<Doctor>> GetAllDoctorsAsync();
    Task<Doctor?> GetDoctorByIdAsync(int id);
    Task<Doctor> AddDoctorAsync(Doctor doctor);
    Task<bool> UpdateDoctorAsync(int id, Doctor doctor);
    Task<bool> DeleteDoctorAsync(int id);
}