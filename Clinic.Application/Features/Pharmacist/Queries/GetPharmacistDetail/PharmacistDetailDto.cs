namespace Clinic.Application.Features.Pharmacist.Queries.GetPharmacistDetail
{
    public class PharmacistDetailDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PharmacyLicenseNumber { get; set; }
    }
}

