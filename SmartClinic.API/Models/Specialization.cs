using System.ComponentModel.DataAnnotations;

namespace SmartClinic.API.Models;

public class Specialization
{
    public int SpecializationId { get; set; }

    [Required]
    public string Name { get; set; } = "";

    public List<DoctorSpecialization> DoctorSpecializations { get; set; } = new();
}