namespace SmartClinic.API.DTO;

public class AppointmentResponseDTO
{
    public int AppointmentId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string Status { get; set; } = "";
    public string Notes { get; set; } = "";

    public int PatientId { get; set; }
    public string? PatientName { get; set; }

    public int DoctorId { get; set; }
    public string? DoctorName { get; set; }
}