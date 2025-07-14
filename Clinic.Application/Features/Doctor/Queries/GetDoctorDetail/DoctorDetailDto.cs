namespace Clinic.Application.Features.Doctor.Queries.GetDoctorDetail
{
    /// <summary>
    /// DTO for detailed doctor information including related data.
    /// Used for doctor detail views and comprehensive doctor information display.
    /// </summary>
    public class DoctorDetailDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string LicenseNumber { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public string? Qualifications { get; set; }
        public decimal ConsultationFee { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        
        // Calculated fields
        public int YearsOfExperience { get; set; }
        
        // Related data counts and summaries
        public int TotalAppointments { get; set; }
        public int UpcomingAppointments { get; set; }
        
        // Availability information could be added here
        // public List<DoctorAvailabilityDto> Availability { get; set; } = new();
        // public List<AppointmentSummaryDto> RecentAppointments { get; set; } = new();
    }
}

