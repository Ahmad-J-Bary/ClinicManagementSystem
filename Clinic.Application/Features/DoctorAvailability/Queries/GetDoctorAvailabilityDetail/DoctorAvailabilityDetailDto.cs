namespace Clinic.Application.Features.DoctorAvailability.Queries.GetDoctorAvailabilityDetail
{
    public class DoctorAvailabilityDetailDto
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsBooked { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public bool IsRecurring { get; set; }
        public DateTime? RecurrenceEndDate { get; set; }
        public string? Notes { get; set; }
        public bool IsEmergencySlot { get; set; }
    }
}

