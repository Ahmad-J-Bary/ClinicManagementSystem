using Clinic.Application.Contracts.Persistence;
using Clinic.Domain.Entities;
using Clinic.Persistence.DatabaseContext;

namespace Clinic.Persistence.Repositories
{
    public class DoctorAvailabilityRepository : GenericRepository<DoctorAvailability>, IDoctorAvailabilityRepository
    {
        public DoctorAvailabilityRepository(ClinicDbContext context) : base(context)
        {
        }
    }
}

