namespace SmartClinic.API.DTO;

public class DoctorCreateDTO
{
    public string FullName { get; set; } = "";
    public string Email { get; set; } = "";
    public string PhoneNumber { get; set; } = "";
    public int DepartmentId { get; set; }
}