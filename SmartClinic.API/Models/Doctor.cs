using System.ComponentModel.DataAnnotations;

namespace SmartClinic.API.Models;

public class Doctor
{
    public int DoctorId { get; set; }

    [Required]
    public string FullName { get; set; } = "";

    public string Email { get; set; } = "";

    public string PhoneNumber { get; set; } = "";

    public int DepartmentId { get; set; }

    public Department? Department { get; set; }

    public List<Appointment> Appointments { get; set; } = new();

    public List<DoctorSpecialization> DoctorSpecializations { get; set; } = new();
}