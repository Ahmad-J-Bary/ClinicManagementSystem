using MediatR;
using Clinic.Domain.Enums;

namespace Clinic.Application.Features.Notification.Commands.CreateNotification
{
    public class CreateNotificationCommand : IRequest<int>
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public NotificationType Type { get; set; }
        public bool IsUrgent { get; set; }
        public string? ActionUrl { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}

