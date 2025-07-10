using System;
using System.Collections.Generic;
using System.Linq;
using Clinic.Domain.Enums;

namespace Clinic.Domain.Entities
{
    /// <summary>
    /// Represents a patient in the clinic management system.
    /// This entity holds patient-specific information and their medical history.
    /// </summary>
    public class Patient : User
    {
        public string PatientIdNumber { get; private set; } // Unique identifier for the patient
        public string? InsuranceProvider { get; private set; }
        public string? InsurancePolicyNumber { get; private set; }
        public string? EmergencyContactName { get; private set; }
        public string? EmergencyContactPhone { get; private set; }
        public string? BloodType { get; private set; }
        public string? Allergies { get; private set; }

        // Navigation properties
        public virtual ICollection<MedicalRecord> MedicalRecords { get; private set; } = new List<MedicalRecord>();
        public virtual ICollection<Appointment> Appointments { get; private set; } = new List<Appointment>();
        public virtual ICollection<Payment> Payments { get; private set; } = new List<Payment>();

        private Patient() : base() { }

        public Patient(string firstName, string lastName, string email, string phoneNumber, 
                      string address, DateTime dateOfBirth, string identityUserId, 
                      string patientIdNumber, string? insuranceProvider = null, 
                      string? insurancePolicyNumber = null)
            : base(firstName, lastName, email, phoneNumber, address, dateOfBirth, identityUserId, UserRole.Patient)
        {
            PatientIdNumber = patientIdNumber ?? throw new ArgumentNullException(nameof(patientIdNumber));
            InsuranceProvider = insuranceProvider;
            InsurancePolicyNumber = insurancePolicyNumber;
        }

        public void UpdateInsuranceInfo(string? insuranceProvider, string? insurancePolicyNumber)
        {
            InsuranceProvider = insuranceProvider;
            InsurancePolicyNumber = insurancePolicyNumber;
        }

        public void UpdateEmergencyContact(string? emergencyContactName, string? emergencyContactPhone)
        {
            EmergencyContactName = emergencyContactName;
            EmergencyContactPhone = emergencyContactPhone;
        }

        public void UpdateMedicalInfo(string? bloodType, string? allergies)
        {
            BloodType = bloodType;
            Allergies = allergies;
        }

        public void AddMedicalRecord(MedicalRecord record)
        {
            if (record == null) throw new ArgumentNullException(nameof(record));
            MedicalRecords.Add(record);
        }

        public void BookAppointment(Appointment appointment)
        {
            if (appointment == null) throw new ArgumentNullException(nameof(appointment));
            if (appointment.PatientId != Id)
                throw new InvalidOperationException("Appointment does not belong to this patient.");
            
            Appointments.Add(appointment);
        }

        public void AddPayment(Payment payment)
        {
            if (payment == null) throw new ArgumentNullException(nameof(payment));
            if (payment.PatientId != Id)
                throw new InvalidOperationException("Payment does not belong to this patient.");
            
            Payments.Add(payment);
        }

        public IEnumerable<Appointment> GetUpcomingAppointments()
        {
            return Appointments.Where(a => a.StartTime > DateTime.UtcNow && 
                                         (a.Status == AppointmentStatus.Confirmed || a.Status == AppointmentStatus.Pending))
                              .OrderBy(a => a.StartTime);
        }

        public IEnumerable<MedicalRecord> GetRecentMedicalRecords(int count = 10)
        {
            return MedicalRecords.OrderByDescending(mr => mr.RecordDate)
                                .Take(count);
        }

        public decimal GetTotalOutstandingBalance()
        {
            return Payments.Where(p => p.Status == PaymentStatus.Pending)
                          .Sum(p => p.Amount);
        }
    }
}

