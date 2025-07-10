using System;
using System.Collections.Generic;
using System.Linq;

namespace Clinic.Domain.Entities
{
    /// <summary>
    /// Represents a patient's medical record, containing diagnoses, treatments, and other health-related information.
    /// </summary>
    public class MedicalRecord : BaseEntity
    {
        public DateTime RecordDate { get; private set; }
        public string Diagnosis { get; private set; }
        public string Treatment { get; private set; }
        public string? Notes { get; private set; }
        public string? Symptoms { get; private set; }
        public string? VitalSigns { get; private set; }
        public string? LabResults { get; private set; }
        public string? ImagingResults { get; private set; }
        public string? FollowUpInstructions { get; private set; }
        public DateTime? NextAppointmentDate { get; private set; }

        // Foreign keys
        public int PatientId { get; private set; }
        public int DoctorId { get; private set; }
        public int? AppointmentId { get; private set; }

        // Navigation properties
        public virtual Patient Patient { get; private set; }
        public virtual Doctor Doctor { get; private set; }
        public virtual Appointment? Appointment { get; private set; }
        public virtual ICollection<Prescription> Prescriptions { get; private set; } = new List<Prescription>();

        private MedicalRecord() { }

        public MedicalRecord(int patientId, int doctorId, DateTime recordDate, 
                           string diagnosis, string treatment, string? notes = null,
                           int? appointmentId = null)
        {
            PatientId = patientId;
            DoctorId = doctorId;
            RecordDate = recordDate;
            Diagnosis = diagnosis ?? throw new ArgumentNullException(nameof(diagnosis));
            Treatment = treatment ?? throw new ArgumentNullException(nameof(treatment));
            Notes = notes;
            AppointmentId = appointmentId;
        }

        public void UpdateDiagnosisAndTreatment(string diagnosis, string treatment)
        {
            Diagnosis = diagnosis ?? throw new ArgumentNullException(nameof(diagnosis));
            Treatment = treatment ?? throw new ArgumentNullException(nameof(treatment));
        }

        public void UpdateNotes(string? notes)
        {
            Notes = notes;
        }

        public void UpdateSymptoms(string? symptoms)
        {
            Symptoms = symptoms;
        }

        public void UpdateVitalSigns(string? vitalSigns)
        {
            VitalSigns = vitalSigns;
        }

        public void UpdateLabResults(string? labResults)
        {
            LabResults = labResults;
        }

        public void UpdateImagingResults(string? imagingResults)
        {
            ImagingResults = imagingResults;
        }

        public void UpdateFollowUpInstructions(string? followUpInstructions, DateTime? nextAppointmentDate = null)
        {
            FollowUpInstructions = followUpInstructions;
            NextAppointmentDate = nextAppointmentDate;
        }

        public void AddPrescription(Prescription prescription)
        {
            if (prescription == null) throw new ArgumentNullException(nameof(prescription));
            
            if (prescription.MedicalRecordId != Id)
                throw new InvalidOperationException("Prescription does not belong to this medical record.");
            
            Prescriptions.Add(prescription);
        }

        public void RemovePrescription(Prescription prescription)
        {
            if (prescription == null) throw new ArgumentNullException(nameof(prescription));
            
            var existingPrescription = Prescriptions.FirstOrDefault(p => p.Id == prescription.Id);
            if (existingPrescription == null)
                throw new InvalidOperationException("Prescription is not associated with this medical record.");
            
            Prescriptions.Remove(existingPrescription);
        }

        public IEnumerable<Prescription> GetActivePrescriptions()
        {
            return Prescriptions.Where(p => p.Status == Enums.PrescriptionStatus.Active && 
                                          p.ExpiryDate > DateTime.UtcNow);
        }

        public IEnumerable<Prescription> GetExpiredPrescriptions()
        {
            return Prescriptions.Where(p => p.ExpiryDate <= DateTime.UtcNow);
        }

        public bool HasActivePrescriptions()
        {
            return GetActivePrescriptions().Any();
        }

        public bool RequiresFollowUp()
        {
            return NextAppointmentDate.HasValue && NextAppointmentDate.Value > DateTime.UtcNow;
        }

        public bool IsRecentRecord(int days = 30)
        {
            return RecordDate >= DateTime.UtcNow.AddDays(-days);
        }

        public void MarkAsAmended(string amendmentReason)
        {
            if (string.IsNullOrWhiteSpace(amendmentReason))
                throw new ArgumentException("Amendment reason is required.", nameof(amendmentReason));
            
            Notes = $"{Notes}\n\n[AMENDED: {DateTime.UtcNow:yyyy-MM-dd HH:mm}] {amendmentReason}";
        }

        public string GetSummary()
        {
            return $"Date: {RecordDate:yyyy-MM-dd}, Diagnosis: {Diagnosis}, Treatment: {Treatment}";
        }
    }
}

