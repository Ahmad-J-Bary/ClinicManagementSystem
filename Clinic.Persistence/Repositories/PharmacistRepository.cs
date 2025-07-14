using Clinic.Application.Contracts.Persistence;
using Clinic.Domain.Entities;
using Clinic.Persistence.DatabaseContext;

namespace Clinic.Persistence.Repositories
{
    public class PharmacistRepository : GenericRepository<Pharmacist>, IPharmacistRepository
    {
        public PharmacistRepository(ClinicDbContext context) : base(context)
        {
        }
    }
}

