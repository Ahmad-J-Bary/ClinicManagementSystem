using Clinic.Application.Contracts.Persistence;
using Clinic.Domain.Entities;
using Clinic.Persistence.DatabaseContext;

namespace Clinic.Persistence.Repositories
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(ClinicDbContext context) : base(context)
        {
        }
    }
}

