using MediatR;

namespace Clinic.Application.Features.Receptionist.Commands.CreateReceptionist
{
    public class CreateReceptionistCommand : IRequest<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string IdentityUserId { get; set; }
        public string EmployeeId { get; set; }
    }
}

