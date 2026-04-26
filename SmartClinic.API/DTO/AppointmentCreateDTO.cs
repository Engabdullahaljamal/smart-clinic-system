namespace SmartClinic.API.DTO;

public class AppointmentCreateDTO
{
    public DateTime AppointmentDate { get; set; }
    public string Status { get; set; } = "Scheduled";
    public string Notes { get; set; } = "";
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
}