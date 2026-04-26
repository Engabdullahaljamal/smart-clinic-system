using System.ComponentModel.DataAnnotations;

namespace SmartClinic.API.Models;

public class Department
{
    public int DepartmentId { get; set; }

    [Required]
    public string Name { get; set; } = "";

    public string Description { get; set; } = "";

    public List<Doctor> Doctors { get; set; } = new();
}