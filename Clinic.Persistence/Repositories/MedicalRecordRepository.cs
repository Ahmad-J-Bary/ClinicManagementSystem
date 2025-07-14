using Clinic.Application.Contracts.Persistence;
using Clinic.Domain.Entities;
using Clinic.Persistence.DatabaseContext;

namespace Clinic.Persistence.Repositories
{
    public class MedicalRecordRepository : GenericRepository<MedicalRecord>, IMedicalRecordRepository
    {
        public MedicalRecordRepository(ClinicDbContext context) : base(context)
        {
        }
    }
}

