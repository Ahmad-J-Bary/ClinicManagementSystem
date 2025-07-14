using MediatR;

namespace Clinic.Application.Features.Doctor.Commands.UpdateDoctor
{
    /// <summary>
    /// Command for updating an existing doctor in the system.
    /// Implements MediatR IRequest pattern for CQRS.
    /// </summary>
    public class UpdateDoctorCommand : IRequest<Unit>
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

