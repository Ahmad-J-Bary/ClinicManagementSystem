using Clinic.Application.Contracts.Persistence;
using Clinic.Domain.Entities;
using Clinic.Persistence.DatabaseContext;

namespace Clinic.Persistence.Repositories
{
    public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(ClinicDbContext context) : base(context)
        {
        }
    }
}

