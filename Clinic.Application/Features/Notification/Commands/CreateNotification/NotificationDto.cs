
using Clinic.Domain.Enums;

namespace Clinic.Application.Features.Notification.Commands.CreateNotification
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public NotificationType Type { get; set; }
        public DateTime SentDate { get; set; }
        public bool IsRead { get; set; }
        public DateTime? ReadDate { get; set; }
        public string? ActionUrl { get; set; }
        public bool IsUrgent { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}

