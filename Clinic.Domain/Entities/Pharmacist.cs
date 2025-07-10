using System;
using System.Collections.Generic;
using System.Linq;
using Clinic.Domain.Enums;

namespace Clinic.Domain.Entities
{
    /// <summary>
    /// Represents a pharmacist associated with the clinic.
    /// This role is responsible for managing prescriptions and dispensing medications.
    /// </summary>
    public class Pharmacist : User
    {
        public string PharmacyLicenseNumber { get; private set; }
        public string? PharmacyName { get; private set; }
        public string? EmployeeId { get; private set; }
        public DateTime? LastLoginDate { get; private set; }
        public string? Specialization { get; private set; } // Clinical Pharmacy, Hospital Pharmacy, etc.

        private Pharmacist() : base() { }

        public Pharmacist(string firstName, string lastName, string email, string phoneNumber, 
                         string address, DateTime dateOfBirth, string identityUserId, 
                         string pharmacyLicenseNumber, string? pharmacyName = null, 
                         string? employeeId = null, string? specialization = null)
            : base(firstName, lastName, email, phoneNumber, address, dateOfBirth, identityUserId, UserRole.Pharmacist)
        {
            PharmacyLicenseNumber = pharmacyLicenseNumber ?? throw new ArgumentNullException(nameof(pharmacyLicenseNumber));
            PharmacyName = pharmacyName;
            EmployeeId = employeeId;
            Specialization = specialization;
        }

        public void UpdateProfessionalInfo(string pharmacyLicenseNumber, string? pharmacyName, 
                                         string? employeeId, string? specialization)
        {
            PharmacyLicenseNumber = pharmacyLicenseNumber ?? throw new ArgumentNullException(nameof(pharmacyLicenseNumber));
            PharmacyName = pharmacyName;
            EmployeeId = employeeId;
            Specialization = specialization;
        }

        public void RecordLogin()
        {
            LastLoginDate = DateTime.UtcNow;
        }

        public void DispensePrescription(Prescription prescription)
        {
            if (prescription == null) throw new ArgumentNullException(nameof(prescription));
            
            if (prescription.Status != PrescriptionStatus.Active)
                throw new InvalidOperationException("Only active prescriptions can be dispensed.");
            
            if (prescription.ExpiryDate <= DateTime.UtcNow)
                throw new InvalidOperationException("Prescription has expired and cannot be dispensed.");
            
            prescription.MarkAsDispensed();
        }

        public bool VerifyPrescription(Prescription prescription)
        {
            if (prescription == null) throw new ArgumentNullException(nameof(prescription));
            
            // Verify prescription validity
            if (prescription.Status != PrescriptionStatus.Active)
                return false;
            
            if (prescription.ExpiryDate <= DateTime.UtcNow)
                return false;
            
            if (string.IsNullOrWhiteSpace(prescription.MedicationName) ||
                string.IsNullOrWhiteSpace(prescription.Dosage) ||
                string.IsNullOrWhiteSpace(prescription.Instructions))
                return false;
            
            return true;
        }

        public void CancelPrescription(Prescription prescription, string reason)
        {
            if (prescription == null) throw new ArgumentNullException(nameof(prescription));
            if (string.IsNullOrWhiteSpace(reason)) throw new ArgumentException("Cancellation reason is required.", nameof(reason));
            
            prescription.MarkAsCancelled();
        }

        public bool CheckDrugInteractions(IEnumerable<Prescription> prescriptions)
        {
            if (prescriptions == null) throw new ArgumentNullException(nameof(prescriptions));
            
            // This would typically involve checking against a drug interaction database
            // For now, returning false (no interactions) as this is domain logic
            // In a real implementation, this would integrate with external drug databases
            
            var activePrescriptions = prescriptions.Where(p => p.Status == PrescriptionStatus.Active).ToList();
            
            // Basic check for duplicate medications
            var medicationNames = activePrescriptions.Select(p => p.MedicationName.ToLower()).ToList();
            var hasDuplicates = medicationNames.Count != medicationNames.Distinct().Count();
            
            return hasDuplicates; // Return true if there are potential interactions (duplicates)
        }

        public void ReviewPrescription(Prescription prescription, string notes)
        {
            if (prescription == null) throw new ArgumentNullException(nameof(prescription));
            
            // Add pharmacist review notes
            // This could be implemented with additional properties or related entities
        }

        public bool CanDispenseMedication()
        {
            return IsActive && !string.IsNullOrWhiteSpace(PharmacyLicenseNumber);
        }

        public bool CanVerifyPrescriptions()
        {
            return IsActive && !string.IsNullOrWhiteSpace(PharmacyLicenseNumber);
        }

        public IEnumerable<Prescription> GetPendingPrescriptions()
        {
            // This would typically be retrieved from a repository
            // For now, returning empty collection as this is domain logic
            return new List<Prescription>();
        }
    }
}

