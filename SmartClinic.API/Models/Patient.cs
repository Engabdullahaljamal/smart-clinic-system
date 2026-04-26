using System.ComponentModel.DataAnnotations;

namespace SmartClinic.API.Models;

public class Patient
{
    public int PatientId { get; set; }

    [Required]
    public string FullName { get; set; } = "";

    public DateTime DateOfBirth { get; set; }

    public string PhoneNumber { get; set; } = "";

    public string Email { get; set; } = "";

    public string Address { get; set; } = "";

    public List<Appointment> Appointments { get; set; } = new();
}