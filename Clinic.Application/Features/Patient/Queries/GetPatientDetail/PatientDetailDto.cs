namespace Clinic.Application.Features.Patient.Queries.GetPatientDetail
{
    /// <summary>
    /// DTO for detailed patient information including related data.
    /// Used for patient detail views and comprehensive patient information display.
    /// </summary>
    public class PatientDetailDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; }
        public string PatientIdNumber { get; set; } = string.Empty;
        public string? InsuranceProvider { get; set; }
        public string? InsurancePolicyNumber { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }
        public string? BloodType { get; set; }
        public string? Allergies { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        
        // Related data counts and summaries
        public int TotalAppointments { get; set; }
        public decimal OutstandingBalance { get; set; }
        
        // Recent appointments and medical records could be added here
        // public List<AppointmentSummaryDto> RecentAppointments { get; set; } = new();
        // public List<MedicalRecordSummaryDto> RecentMedicalRecords { get; set; } = new();
    }
}

