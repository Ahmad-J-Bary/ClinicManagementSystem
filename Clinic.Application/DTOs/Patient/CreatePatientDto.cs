using System.ComponentModel.DataAnnotations;

namespace Clinic.Application.DTOs.Patient
{
    /// <summary>
    /// Data Transfer Object for creating a new Patient.
    /// </summary>
    public class CreatePatientDto
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Phone]
        [StringLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Address { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(20)]
        public string PatientIdNumber { get; set; } = string.Empty;

        [StringLength(100)]
        public string? InsuranceProvider { get; set; }

        [StringLength(50)]
        public string? InsurancePolicyNumber { get; set; }

        [StringLength(100)]
        public string? EmergencyContactName { get; set; }

        [Phone]
        [StringLength(20)]
        public string? EmergencyContactPhone { get; set; }

        [StringLength(10)]
        public string? BloodType { get; set; }

        [StringLength(500)]
        public string? Allergies { get; set; }

        [Required]
        public string IdentityUserId { get; set; } = string.Empty;
    }
}

