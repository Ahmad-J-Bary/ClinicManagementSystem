namespace Clinic.Application.Features.Receptionist.Queries.GetReceptionistDetail
{
    public class ReceptionistDetailDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string EmployeeId { get; set; }
    }
}

