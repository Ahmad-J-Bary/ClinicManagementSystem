using MediatR;

namespace Clinic.Application.Features.DoctorAvailability.Commands.CreateDoctorAvailability
{
    public class CreateDoctorAvailabilityCommand : IRequest<int>
    {
        public int DoctorId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsRecurring { get; set; }
        public DateTime? RecurrenceEndDate { get; set; }
        public bool IsEmergencySlot { get; set; }
        public string? Notes { get; set; }
    }
}

