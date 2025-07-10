using System;
using System.Collections.Generic;
using System.Linq;
using Clinic.Domain.Enums;

namespace Clinic.Domain.Entities
{
    /// <summary>
    /// Represents a receptionist at the clinic.
    /// This role is primarily involved in managing appointments, patient check-ins, and general clinic operations.
    /// </summary>
    public class Receptionist : User
    {
        public string? EmployeeId { get; private set; }
        public string? WorkShift { get; private set; } // Morning, Evening, Night
        public DateTime? LastLoginDate { get; private set; }

        private Receptionist() : base() { }

        public Receptionist(string firstName, string lastName, string email, string phoneNumber, 
                           string address, DateTime dateOfBirth, string identityUserId, 
                           string? employeeId = null, string? workShift = null)
            : base(firstName, lastName, email, phoneNumber, address, dateOfBirth, identityUserId, UserRole.Receptionist)
        {
            EmployeeId = employeeId;
            WorkShift = workShift;
        }

        public void UpdateEmployeeInfo(string? employeeId, string? workShift)
        {
            EmployeeId = employeeId;
            WorkShift = workShift;
        }

        public void RecordLogin()
        {
            LastLoginDate = DateTime.UtcNow;
        }

        public Appointment ScheduleAppointment(Patient patient, Doctor doctor, DateTime startTime, 
                                             DateTime endTime, string reason)
        {
            if (patient == null) throw new ArgumentNullException(nameof(patient));
            if (doctor == null) throw new ArgumentNullException(nameof(doctor));
            if (string.IsNullOrWhiteSpace(reason)) throw new ArgumentException("Reason is required.", nameof(reason));
            if (startTime >= endTime) throw new ArgumentException("Start time must be before end time.");
            if (startTime <= DateTime.UtcNow) throw new ArgumentException("Appointment cannot be scheduled in the past.");

            // Check if doctor is available
            if (!doctor.IsAvailable(startTime, endTime))
                throw new InvalidOperationException("Doctor is not available at the requested time.");

            var appointment = new Appointment(startTime, endTime, reason, patient.Id, doctor.Id);
            
            // Auto-confirm appointments scheduled by receptionist
            appointment.Confirm();
            
            return appointment;
        }

        public void CheckInPatient(Patient patient, Appointment appointment)
        {
            if (patient == null) throw new ArgumentNullException(nameof(patient));
            if (appointment == null) throw new ArgumentNullException(nameof(appointment));
            
            if (appointment.PatientId != patient.Id)
                throw new InvalidOperationException("Appointment does not belong to this patient.");
            
            if (appointment.Status != AppointmentStatus.Confirmed)
                throw new InvalidOperationException("Only confirmed appointments can be checked in.");
            
            // Mark appointment as in progress or checked in
            // This could be implemented with additional status or property
        }

        public void CancelAppointment(Appointment appointment, string reason)
        {
            if (appointment == null) throw new ArgumentNullException(nameof(appointment));
            if (string.IsNullOrWhiteSpace(reason)) throw new ArgumentException("Cancellation reason is required.", nameof(reason));
            
            appointment.Cancel(reason);
        }

        public void RescheduleAppointment(Appointment appointment, DateTime newStartTime, DateTime newEndTime)
        {
            if (appointment == null) throw new ArgumentNullException(nameof(appointment));
            if (newStartTime >= newEndTime) throw new ArgumentException("Start time must be before end time.");
            if (newStartTime <= DateTime.UtcNow) throw new ArgumentException("Appointment cannot be rescheduled to the past.");
            
            if (appointment.Status != AppointmentStatus.Confirmed && appointment.Status != AppointmentStatus.Pending)
                throw new InvalidOperationException("Only confirmed or pending appointments can be rescheduled.");
            
            appointment.Reschedule(newStartTime, newEndTime);
        }

        public IEnumerable<Appointment> GetTodaysAppointments()
        {
            // This would typically be retrieved from a repository
            // For now, returning empty collection as this is domain logic
            return new List<Appointment>();
        }

        public bool CanManageAppointments()
        {
            return IsActive;
        }

        public bool CanCheckInPatients()
        {
            return IsActive;
        }
    }
}

