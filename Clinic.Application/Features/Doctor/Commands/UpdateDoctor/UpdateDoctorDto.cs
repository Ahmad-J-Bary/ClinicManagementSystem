namespace Clinic.Application.DTOs.Doctor
{
    /// <summary>
    /// DTO for updating doctor information.
    /// Contains all updatable fields for a doctor.
    /// </summary>
    public class UpdateDoctorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Specialization { get; set; } = string.Empty;
        public int DepartmentId { get; set; }
        public string? Qualifications { get; set; }
        public decimal ConsultationFee { get; set; }
    }
}

