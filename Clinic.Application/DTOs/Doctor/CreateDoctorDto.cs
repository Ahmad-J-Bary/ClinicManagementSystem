namespace Clinic.Application.DTOs.Doctor
{
    /// <summary>
    /// DTO for creating a new doctor in the system.
    /// Contains all required and optional fields for doctor registration.
    /// </summary>
    public class CreateDoctorDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string IdentityUserId { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public int DepartmentId { get; set; }
        public string? Qualifications { get; set; }
        public decimal ConsultationFee { get; set; }
    }
}

