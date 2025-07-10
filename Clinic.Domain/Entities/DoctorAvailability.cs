using System;

namespace Clinic.Domain.Entities
{
    /// <summary>
    /// Represents a time slot when a doctor is available for appointments.
    /// This entity manages doctor scheduling and availability.
    /// </summary>
    public class DoctorAvailability : BaseEntity
    {
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public bool IsBooked { get; private set; }
        public DayOfWeek DayOfWeek { get; private set; }
        public bool IsRecurring { get; private set; }
        public DateTime? RecurrenceEndDate { get; private set; }
        public string? Notes { get; private set; }
        public bool IsEmergencySlot { get; private set; }

        // Foreign key
        public int DoctorId { get; private set; }

        // Navigation property
        public virtual Doctor Doctor { get; private set; }

        private DoctorAvailability() { }

        public DoctorAvailability(int doctorId, DateTime startTime, DateTime endTime, 
                                bool isRecurring = false, DateTime? recurrenceEndDate = null,
                                bool isEmergencySlot = false, string? notes = null)
        {
            if (startTime >= endTime)
                throw new ArgumentException("Start time must be before end time.");
            
            if (startTime <= DateTime.UtcNow)
                throw new ArgumentException("Availability cannot be set in the past.");
            
            DoctorId = doctorId;
            StartTime = startTime;
            EndTime = endTime;
            DayOfWeek = startTime.DayOfWeek;
            IsBooked = false;
            IsRecurring = isRecurring;
            RecurrenceEndDate = recurrenceEndDate;
            IsEmergencySlot = isEmergencySlot;
            Notes = notes;

            if (isRecurring && recurrenceEndDate.HasValue && recurrenceEndDate.Value <= startTime)
                throw new ArgumentException("Recurrence end date must be after start time.");
        }

        public void MarkAsBooked()
        {
            if (IsBooked)
                throw new InvalidOperationException("Availability slot is already booked.");
            
            if (StartTime <= DateTime.UtcNow)
                throw new InvalidOperationException("Cannot book past availability slots.");
            
            IsBooked = true;
        }

        public void MarkAsAvailable()
        {
            IsBooked = false;
        }

        public void UpdateTimeSlot(DateTime newStartTime, DateTime newEndTime)
        {
            if (IsBooked)
                throw new InvalidOperationException("Cannot modify booked availability slots.");
            
            if (newStartTime >= newEndTime)
                throw new ArgumentException("Start time must be before end time.");
            
            if (newStartTime <= DateTime.UtcNow)
                throw new ArgumentException("Availability cannot be set in the past.");
            
            StartTime = newStartTime;
            EndTime = newEndTime;
            DayOfWeek = newStartTime.DayOfWeek;
        }

        public void UpdateNotes(string? notes)
        {
            Notes = notes;
        }

        public void MarkAsEmergencySlot()
        {
            IsEmergencySlot = true;
        }

        public void RemoveEmergencySlotStatus()
        {
            IsEmergencySlot = false;
        }

        public void SetRecurring(bool isRecurring, DateTime? recurrenceEndDate = null)
        {
            if (IsBooked)
                throw new InvalidOperationException("Cannot modify recurrence settings for booked slots.");
            
            IsRecurring = isRecurring;
            RecurrenceEndDate = recurrenceEndDate;
            
            if (isRecurring && recurrenceEndDate.HasValue && recurrenceEndDate.Value <= StartTime)
                throw new ArgumentException("Recurrence end date must be after start time.");
        }

        public void ExtendRecurrence(DateTime newRecurrenceEndDate)
        {
            if (!IsRecurring)
                throw new InvalidOperationException("Cannot extend recurrence for non-recurring availability.");
            
            if (newRecurrenceEndDate <= StartTime)
                throw new ArgumentException("New recurrence end date must be after start time.");
            
            if (RecurrenceEndDate.HasValue && newRecurrenceEndDate <= RecurrenceEndDate.Value)
                throw new ArgumentException("New recurrence end date must be after current recurrence end date.");
            
            RecurrenceEndDate = newRecurrenceEndDate;
        }

        public TimeSpan GetDuration()
        {
            return EndTime - StartTime;
        }

        public bool IsAvailableNow()
        {
            var now = DateTime.UtcNow;
            return !IsBooked && StartTime <= now && EndTime > now;
        }

        public bool IsUpcoming()
        {
            return StartTime > DateTime.UtcNow;
        }

        public bool IsPast()
        {
            return EndTime <= DateTime.UtcNow;
        }

        public bool IsToday()
        {
            return StartTime.Date == DateTime.UtcNow.Date;
        }

        public bool IsThisWeek()
        {
            var now = DateTime.UtcNow;
            var startOfWeek = now.AddDays(-(int)now.DayOfWeek);
            var endOfWeek = startOfWeek.AddDays(7);
            
            return StartTime >= startOfWeek && StartTime < endOfWeek;
        }

        public bool OverlapsWith(DateTime otherStartTime, DateTime otherEndTime)
        {
            return StartTime < otherEndTime && EndTime > otherStartTime;
        }

        public bool OverlapsWith(DoctorAvailability other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            return OverlapsWith(other.StartTime, other.EndTime);
        }

        public bool CanAccommodateAppointment(TimeSpan appointmentDuration)
        {
            return !IsBooked && GetDuration() >= appointmentDuration && IsUpcoming();
        }

        public string GetTimeSlotDisplay()
        {
            return $"{StartTime:MMM dd, yyyy h:mm tt} - {EndTime:h:mm tt}";
        }

        public string GetDayTimeDisplay()
        {
            return $"{DayOfWeek} {StartTime:h:mm tt} - {EndTime:h:mm tt}";
        }

        public bool IsActiveRecurrence()
        {
            if (!IsRecurring)
                return false;
            
            if (!RecurrenceEndDate.HasValue)
                return true;
            
            return DateTime.UtcNow <= RecurrenceEndDate.Value;
        }

        public bool ShouldGenerateNextRecurrence()
        {
            return IsRecurring && IsActiveRecurrence() && StartTime.Date <= DateTime.UtcNow.Date;
        }
    }
}

