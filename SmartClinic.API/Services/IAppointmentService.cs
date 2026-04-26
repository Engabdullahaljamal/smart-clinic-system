using SmartClinic.API.Models;

namespace SmartClinic.API.Services;

public interface IAppointmentService
{
    Task<List<Appointment>> GetAllAppointmentsAsync();
    Task<Appointment?> GetAppointmentByIdAsync(int id);
    Task<Appointment> AddAppointmentAsync(Appointment appointment);
    Task<bool> DeleteAppointmentAsync(int id);
}