namespace SmartClinic.API.DTO;

public class DoctorResponseDTO
{
    public int DoctorId { get; set; }
    public string FullName { get; set; } = "";
    public string Email { get; set; } = "";
    public string PhoneNumber { get; set; } = "";
    public int DepartmentId { get; set; }
    public string? DepartmentName { get; set; }
}