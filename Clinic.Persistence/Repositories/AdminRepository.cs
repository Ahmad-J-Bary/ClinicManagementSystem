using Clinic.Application.Contracts.Persistence;
using Clinic.Domain.Entities;
using Clinic.Persistence.DatabaseContext;

namespace Clinic.Persistence.Repositories
{
    public class AdminRepository : GenericRepository<Admin>, IAdminRepository
    {
        public AdminRepository(ClinicDbContext context) : base(context)
        {
        }
    }
}

