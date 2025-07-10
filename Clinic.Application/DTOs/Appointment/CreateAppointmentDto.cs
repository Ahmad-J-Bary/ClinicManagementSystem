using System.ComponentModel.DataAnnotations;

namespace Clinic.Application.DTOs.Appointment
{
    /// <summary>
    /// Data Transfer Object for creating a new Appointment.
    /// </summary>
    public class CreateAppointmentDto
    {
        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        [StringLength(500)]
        public string Reason { get; set; } = string.Empty;

        [Required]
        public int PatientId { get; set; }

        [Required]
        public int DoctorId { get; set; }

        public bool IsEmergency { get; set; } = false;

        [StringLength(1000)]
        public string? Notes { get; set; }
    }
}

