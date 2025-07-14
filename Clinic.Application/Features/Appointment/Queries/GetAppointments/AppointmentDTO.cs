using Clinic.Domain.Enums;

namespace Clinic.Application.Features.Appointment.Queries.GetAppointments;

public class AppointmentDTO
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Reason { get; set; } = string.Empty;
    public AppointmentStatus Status { get; set; }
    public string? Notes { get; set; }
    public string? CancellationReason { get; set; }
    public DateTime? ConfirmedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public bool IsEmergency { get; set; }
    public int PatientId { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public int DoctorId { get; set; }
    public string DoctorName { get; set; } = string.Empty;
    public string DoctorSpecialization { get; set; } = string.Empty;
    public DateTime DateCreated { get; set; }
    public string StatusDisplayName => GetStatusDisplayName();
    public TimeSpan Duration => EndTime - StartTime;
    public bool IsUpcoming => StartTime > DateTime.UtcNow && (Status == AppointmentStatus.Confirmed || Status == AppointmentStatus.Pending);
    public bool IsToday => StartTime.Date == DateTime.UtcNow.Date;
    
    private string GetStatusDisplayName()
    {
        return Status switch
        {
            AppointmentStatus.Pending => "Pending",
            AppointmentStatus.Confirmed => "Confirmed",
            AppointmentStatus.Cancelled => "Cancelled",
            AppointmentStatus.Rejected => "Rejected",
            AppointmentStatus.Completed => "Completed",
            AppointmentStatus.NoShow => "No Show",
            _ => "Unknown"
        };
    }
}