namespace SmartClinic.API.DTO;

public class PatientResponseDTO
{
    public int PatientId { get; set; }
    public string FullName { get; set; } = "";
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; } = "";
    public string Email { get; set; } = "";
    public string Address { get; set; } = "";
}