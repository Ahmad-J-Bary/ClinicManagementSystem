using System;
using System.Collections.Generic;
using System.Linq;
using Clinic.Domain.Enums;

namespace Clinic.Domain.Entities
{
    /// <summary>
    /// Represents a medical doctor in the clinic management system.
    /// This entity includes details specific to a doctor's practice and schedule.
    /// </summary>
    public class Doctor : User
    {
        public string MedicalLicenseNumber { get; private set; }
        public string Specialization { get; private set; }
        public string Qualifications { get; private set; }
        public decimal ConsultationFee { get; private set; }
        public int YearsOfExperience { get; private set; }
        public string? Biography { get; private set; }

        // Foreign key
        public int DepartmentId { get; private set; }

        // Navigation properties
        public virtual Department Department { get; private set; }
        public virtual ICollection<Appointment> Appointments { get; private set; } = new List<Appointment>();
        public virtual ICollection<DoctorAvailability> Availabilities { get; private set; } = new List<DoctorAvailability>();
        public virtual ICollection<MedicalRecord> MedicalRecords { get; private set; } = new List<MedicalRecord>();

        private Doctor() : base() { }

        public Doctor(string firstName, string lastName, string email, string phoneNumber, 
                     string address, DateTime dateOfBirth, string identityUserId, 
                     string medicalLicenseNumber, string specialization, string qualifications,
                     decimal consultationFee, int yearsOfExperience, int departmentId)
            : base(firstName, lastName, email, phoneNumber, address, dateOfBirth, identityUserId, UserRole.Doctor)
        {
            MedicalLicenseNumber = medicalLicenseNumber ?? throw new ArgumentNullException(nameof(medicalLicenseNumber));
            Specialization = specialization ?? throw new ArgumentNullException(nameof(specialization));
            Qualifications = qualifications ?? throw new ArgumentNullException(nameof(qualifications));
            ConsultationFee = consultationFee >= 0 ? consultationFee : throw new ArgumentException("Consultation fee cannot be negative.");
            YearsOfExperience = yearsOfExperience >= 0 ? yearsOfExperience : throw new ArgumentException("Years of experience cannot be negative.");
            DepartmentId = departmentId;
        }

        public void UpdateProfessionalInfo(string specialization, string qualifications, 
                                         decimal consultationFee, int yearsOfExperience, string? biography)
        {
            Specialization = specialization ?? throw new ArgumentNullException(nameof(specialization));
            Qualifications = qualifications ?? throw new ArgumentNullException(nameof(qualifications));
            ConsultationFee = consultationFee >= 0 ? consultationFee : throw new ArgumentException("Consultation fee cannot be negative.");
            YearsOfExperience = yearsOfExperience >= 0 ? yearsOfExperience : throw new ArgumentException("Years of experience cannot be negative.");
            Biography = biography;
        }

        public void UpdateDepartment(int departmentId)
        {
            DepartmentId = departmentId;
        }

        public void AddAvailability(DoctorAvailability availability)
        {
            if (availability == null) throw new ArgumentNullException(nameof(availability));
            if (availability.DoctorId != Id)
                throw new InvalidOperationException("Availability does not belong to this doctor.");
            
            // Check for overlapping availability
            var overlapping = Availabilities.Any(a => 
                a.StartTime < availability.EndTime && a.EndTime > availability.StartTime);
            
            if (overlapping)
                throw new InvalidOperationException("Availability overlaps with existing availability.");
            
            Availabilities.Add(availability);
        }

        public void ConfirmAppointment(Appointment appointment)
        {
            if (appointment == null) throw new ArgumentNullException(nameof(appointment));
            if (appointment.DoctorId != Id)
                throw new InvalidOperationException("Appointment does not belong to this doctor.");
            
            appointment.Confirm();
        }

        public void RejectAppointment(Appointment appointment, string reason)
        {
            if (appointment == null) throw new ArgumentNullException(nameof(appointment));
            if (appointment.DoctorId != Id)
                throw new InvalidOperationException("Appointment does not belong to this doctor.");
            
            appointment.Reject(reason);
        }

        public void CompleteAppointment(Appointment appointment)
        {
            if (appointment == null) throw new ArgumentNullException(nameof(appointment));
            if (appointment.DoctorId != Id)
                throw new InvalidOperationException("Appointment does not belong to this doctor.");
            
            appointment.Complete();
        }

        public void CreateMedicalRecord(MedicalRecord medicalRecord)
        {
            if (medicalRecord == null) throw new ArgumentNullException(nameof(medicalRecord));
            if (medicalRecord.DoctorId != Id)
                throw new InvalidOperationException("Medical record does not belong to this doctor.");
            
            MedicalRecords.Add(medicalRecord);
        }

        public IEnumerable<Appointment> GetTodaysAppointments()
        {
            var today = DateTime.UtcNow.Date;
            return Appointments.Where(a => a.StartTime.Date == today && 
                                         a.Status == AppointmentStatus.Confirmed)
                              .OrderBy(a => a.StartTime);
        }

        public IEnumerable<Appointment> GetUpcomingAppointments(int days = 7)
        {
            var endDate = DateTime.UtcNow.AddDays(days);
            return Appointments.Where(a => a.StartTime > DateTime.UtcNow && 
                                         a.StartTime <= endDate &&
                                         a.Status == AppointmentStatus.Confirmed)
                              .OrderBy(a => a.StartTime);
        }

        public IEnumerable<DoctorAvailability> GetAvailableSlots(DateTime startDate, DateTime endDate)
        {
            return Availabilities.Where(a => a.StartTime >= startDate && 
                                           a.EndTime <= endDate && 
                                           !a.IsBooked)
                                .OrderBy(a => a.StartTime);
        }

        public bool IsAvailable(DateTime startTime, DateTime endTime)
        {
            return Availabilities.Any(a => a.StartTime <= startTime && 
                                         a.EndTime >= endTime && 
                                         !a.IsBooked);
        }
    }
}

