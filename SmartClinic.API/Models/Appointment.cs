using System.ComponentModel.DataAnnotations;

namespace SmartClinic.API.Models;

public class Appointment
{
    public int AppointmentId { get; set; }

    public DateTime AppointmentDate { get; set; }

    public string Status { get; set; } = "Scheduled";

    public string Notes { get; set; } = "";

    public int PatientId { get; set; }

    public Patient? Patient { get; set; }

    public int DoctorId { get; set; }

    public Doctor? Doctor { get; set; }
}