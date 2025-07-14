using MediatR;

namespace Clinic.Application.Features.Patient.Commands.CreatePatient
{
    /// <summary>
    /// Command for creating a new patient in the system.
    /// Implements MediatR IRequest pattern for CQRS.
    /// </summary>
    public class CreatePatientCommand : IRequest<int>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string IdentityUserId { get; set; } = string.Empty;
        public string PatientIdNumber { get; set; } = string.Empty;
        public string? InsuranceProvider { get; set; }
        public string? InsurancePolicyNumber { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }
        public string? BloodType { get; set; }
        public string? Allergies { get; set; }
    }
}

