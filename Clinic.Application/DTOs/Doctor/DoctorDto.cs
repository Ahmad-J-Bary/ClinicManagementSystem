namespace Clinic.Application.DTOs.Doctor
{
    /// <summary>
    /// Data Transfer Object for Doctor entity.
    /// </summary>
    public class DoctorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string MedicalLicenseNumber { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public string Qualifications { get; set; } = string.Empty;
        public decimal ConsultationFee { get; set; }
        public int YearsOfExperience { get; set; }
        public string? Biography { get; set; }
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }
        public string FullName => $"{FirstName} {LastName}";
    }
}

