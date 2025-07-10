using System;
using Clinic.Domain.Enums;

namespace Clinic.Domain.Entities
{
    /// <summary>
    /// Represents a prescription issued by a doctor.
    /// </summary>
    public class Prescription : BaseEntity
    {
        public string MedicationName { get; private set; }
        public string Dosage { get; private set; }
        public string Instructions { get; private set; }
        public int Quantity { get; private set; }
        public int Refills { get; private set; }
        public DateTime IssueDate { get; private set; }
        public DateTime ExpiryDate { get; private set; }
        public PrescriptionStatus Status { get; private set; }
        public string? PharmacistNotes { get; private set; }
        public DateTime? DispensedDate { get; private set; }
        public string? DispensedBy { get; private set; }
        public string? GenericSubstitute { get; private set; }
        public decimal? Cost { get; private set; }

        // Foreign key
        public int MedicalRecordId { get; private set; }

        // Navigation property
        public virtual MedicalRecord MedicalRecord { get; private set; }

        private Prescription() { }

        public Prescription(int medicalRecordId, string medicationName, string dosage, 
                          string instructions, int quantity, int refills, 
                          DateTime issueDate, DateTime expiryDate, decimal? cost = null)
        {
            MedicalRecordId = medicalRecordId;
            MedicationName = medicationName ?? throw new ArgumentNullException(nameof(medicationName));
            Dosage = dosage ?? throw new ArgumentNullException(nameof(dosage));
            Instructions = instructions ?? throw new ArgumentNullException(nameof(instructions));
            Quantity = quantity > 0 ? quantity : throw new ArgumentException("Quantity must be greater than zero.");
            Refills = refills >= 0 ? refills : throw new ArgumentException("Refills cannot be negative.");
            IssueDate = issueDate;
            ExpiryDate = expiryDate > issueDate ? expiryDate : throw new ArgumentException("Expiry date must be after issue date.");
            Status = PrescriptionStatus.Active;
            Cost = cost;
        }

        public void MarkAsDispensed(string dispensedBy = null)
        {
            if (Status != PrescriptionStatus.Active)
                throw new InvalidOperationException("Only active prescriptions can be dispensed.");
            
            if (ExpiryDate <= DateTime.UtcNow)
                throw new InvalidOperationException("Expired prescriptions cannot be dispensed.");
            
            Status = PrescriptionStatus.Dispensed;
            DispensedDate = DateTime.UtcNow;
            DispensedBy = dispensedBy;
        }

        public void MarkAsCancelled()
        {
            if (Status == PrescriptionStatus.Dispensed)
                throw new InvalidOperationException("Dispensed prescriptions cannot be cancelled.");
            
            Status = PrescriptionStatus.Cancelled;
        }

        public void MarkAsExpired()
        {
            if (Status == PrescriptionStatus.Dispensed)
                throw new InvalidOperationException("Dispensed prescriptions cannot be marked as expired.");
            
            Status = PrescriptionStatus.Expired;
        }

        public void UpdatePharmacistNotes(string notes)
        {
            PharmacistNotes = notes;
        }

        public void SetGenericSubstitute(string genericSubstitute)
        {
            if (Status == PrescriptionStatus.Dispensed)
                throw new InvalidOperationException("Cannot modify dispensed prescriptions.");
            
            GenericSubstitute = genericSubstitute;
        }

        public void UpdateCost(decimal cost)
        {
            if (cost < 0)
                throw new ArgumentException("Cost cannot be negative.");
            
            Cost = cost;
        }

        public void UpdateQuantity(int newQuantity)
        {
            if (Status == PrescriptionStatus.Dispensed)
                throw new InvalidOperationException("Cannot modify dispensed prescriptions.");
            
            if (newQuantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");
            
            Quantity = newQuantity;
        }

        public void UpdateRefills(int newRefills)
        {
            if (Status == PrescriptionStatus.Dispensed)
                throw new InvalidOperationException("Cannot modify dispensed prescriptions.");
            
            if (newRefills < 0)
                throw new ArgumentException("Refills cannot be negative.");
            
            Refills = newRefills;
        }

        public void ExtendExpiryDate(DateTime newExpiryDate)
        {
            if (Status == PrescriptionStatus.Dispensed)
                throw new InvalidOperationException("Cannot modify dispensed prescriptions.");
            
            if (newExpiryDate <= IssueDate)
                throw new ArgumentException("New expiry date must be after issue date.");
            
            if (newExpiryDate <= DateTime.UtcNow)
                throw new ArgumentException("New expiry date must be in the future.");
            
            ExpiryDate = newExpiryDate;
            
            // Reactivate if it was expired
            if (Status == PrescriptionStatus.Expired)
                Status = PrescriptionStatus.Active;
        }

        public bool IsExpired()
        {
            return ExpiryDate <= DateTime.UtcNow;
        }

        public bool IsActive()
        {
            return Status == PrescriptionStatus.Active && !IsExpired();
        }

        public bool CanBeDispensed()
        {
            return Status == PrescriptionStatus.Active && !IsExpired();
        }

        public bool HasRefillsRemaining()
        {
            return Refills > 0;
        }

        public int GetDaysUntilExpiry()
        {
            if (IsExpired())
                return 0;
            
            return (int)(ExpiryDate - DateTime.UtcNow).TotalDays;
        }

        public string GetDisplayName()
        {
            return string.IsNullOrWhiteSpace(GenericSubstitute) ? MedicationName : $"{MedicationName} (Generic: {GenericSubstitute})";
        }

        public string GetDosageInstructions()
        {
            return $"{Dosage} - {Instructions}";
        }

        public void CheckAndUpdateExpiredStatus()
        {
            if (IsExpired() && Status == PrescriptionStatus.Active)
            {
                Status = PrescriptionStatus.Expired;
            }
        }
    }
}

