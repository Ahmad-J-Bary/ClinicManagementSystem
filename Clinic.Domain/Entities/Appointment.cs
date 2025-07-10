using System;
using Clinic.Domain.Enums;

namespace Clinic.Domain.Entities
{
    /// <summary>
    /// Represents a scheduled appointment between a patient and a doctor.
    /// This is a crucial aggregate root in the domain.
    /// </summary>
    public class Appointment : BaseEntity
    {
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public string Reason { get; private set; }
        public AppointmentStatus Status { get; private set; }
        public string? Notes { get; private set; }
        public string? CancellationReason { get; private set; }
        public DateTime? ConfirmedAt { get; private set; }
        public DateTime? CompletedAt { get; private set; }
        public bool IsEmergency { get; private set; }

        // Foreign keys
        public int PatientId { get; private set; }
        public int DoctorId { get; private set; }

        // Navigation properties
        public virtual Patient Patient { get; private set; }
        public virtual Doctor Doctor { get; private set; }

        private Appointment() { }

        public Appointment(DateTime startTime, DateTime endTime, string reason, 
                          int patientId, int doctorId, bool isEmergency = false)
        {
            if (startTime >= endTime)
                throw new ArgumentException("Start time must be before end time.");
            
            if (startTime <= DateTime.UtcNow)
                throw new ArgumentException("Appointment cannot be scheduled in the past.");
            
            StartTime = startTime;
            EndTime = endTime;
            Reason = reason ?? throw new ArgumentNullException(nameof(reason));
            PatientId = patientId;
            DoctorId = doctorId;
            Status = AppointmentStatus.Pending;
            IsEmergency = isEmergency;
        }

        public void Confirm()
        {
            if (Status != AppointmentStatus.Pending)
                throw new InvalidOperationException("Only pending appointments can be confirmed.");
            
            Status = AppointmentStatus.Confirmed;
            ConfirmedAt = DateTime.UtcNow;
        }

        public void Cancel(string reason)
        {
            if (Status == AppointmentStatus.Cancelled)
                throw new InvalidOperationException("Appointment is already cancelled.");
            
            if (Status == AppointmentStatus.Completed)
                throw new InvalidOperationException("Completed appointments cannot be cancelled.");
            
            Status = AppointmentStatus.Cancelled;
            CancellationReason = reason ?? throw new ArgumentNullException(nameof(reason));
        }

        public void Reject(string reason)
        {
            if (Status != AppointmentStatus.Pending)
                throw new InvalidOperationException("Only pending appointments can be rejected.");
            
            Status = AppointmentStatus.Rejected;
            CancellationReason = reason ?? throw new ArgumentNullException(nameof(reason));
        }

        public void Complete()
        {
            if (Status != AppointmentStatus.Confirmed)
                throw new InvalidOperationException("Only confirmed appointments can be completed.");
            
            Status = AppointmentStatus.Completed;
            CompletedAt = DateTime.UtcNow;
        }

        public void MarkAsNoShow()
        {
            if (Status != AppointmentStatus.Confirmed)
                throw new InvalidOperationException("Only confirmed appointments can be marked as no-show.");
            
            if (StartTime > DateTime.UtcNow)
                throw new InvalidOperationException("Cannot mark future appointments as no-show.");
            
            Status = AppointmentStatus.NoShow;
        }

        public void Reschedule(DateTime newStartTime, DateTime newEndTime)
        {
            if (newStartTime >= newEndTime)
                throw new ArgumentException("Start time must be before end time.");
            
            if (newStartTime <= DateTime.UtcNow)
                throw new ArgumentException("Appointment cannot be rescheduled to the past.");
            
            if (Status != AppointmentStatus.Confirmed && Status != AppointmentStatus.Pending)
                throw new InvalidOperationException("Only confirmed or pending appointments can be rescheduled.");
            
            StartTime = newStartTime;
            EndTime = newEndTime;
            
            // Reset status to pending if it was confirmed
            if (Status == AppointmentStatus.Confirmed)
            {
                Status = AppointmentStatus.Pending;
                ConfirmedAt = null;
            }
        }

        public void AddNotes(string notes)
        {
            Notes = notes;
        }

        public void MarkAsEmergency()
        {
            IsEmergency = true;
        }

        public void RemoveEmergencyStatus()
        {
            IsEmergency = false;
        }

        public TimeSpan GetDuration()
        {
            return EndTime - StartTime;
        }

        public bool IsUpcoming()
        {
            return StartTime > DateTime.UtcNow && 
                   (Status == AppointmentStatus.Confirmed || Status == AppointmentStatus.Pending);
        }

        public bool IsToday()
        {
            return StartTime.Date == DateTime.UtcNow.Date;
        }

        public bool CanBeCancelled()
        {
            return Status == AppointmentStatus.Pending || Status == AppointmentStatus.Confirmed;
        }

        public bool CanBeRescheduled()
        {
            return Status == AppointmentStatus.Pending || Status == AppointmentStatus.Confirmed;
        }

        public bool IsOverdue()
        {
            return EndTime < DateTime.UtcNow && Status == AppointmentStatus.Confirmed;
        }
    }
}

