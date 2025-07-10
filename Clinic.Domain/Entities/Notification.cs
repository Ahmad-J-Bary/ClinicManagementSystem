using System;
using Clinic.Domain.Enums;

namespace Clinic.Domain.Entities
{
    /// <summary>
    /// Represents a notification sent to a user (patient, doctor, admin, etc.).
    /// </summary>
    public class Notification : BaseEntity
    {
        public string Title { get; private set; }
        public string Message { get; private set; }
        public NotificationType Type { get; private set; }
        public DateTime SentDate { get; private set; }
        public bool IsRead { get; private set; }
        public DateTime? ReadDate { get; private set; }
        public string? ActionUrl { get; private set; }
        public bool IsUrgent { get; private set; }
        public DateTime? ExpiryDate { get; private set; }

        // Foreign key
        public int UserId { get; private set; }

        // Navigation property
        public virtual User User { get; private set; }

        private Notification() { }

        public Notification(int userId, string title, string message, NotificationType type, 
                          bool isUrgent = false, string? actionUrl = null, DateTime? expiryDate = null)
        {
            UserId = userId;
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Type = type;
            SentDate = DateTime.UtcNow;
            IsRead = false;
            IsUrgent = isUrgent;
            ActionUrl = actionUrl;
            ExpiryDate = expiryDate;
        }

        public void MarkAsRead()
        {
            if (!IsRead)
            {
                IsRead = true;
                ReadDate = DateTime.UtcNow;
            }
        }

        public void MarkAsUnread()
        {
            IsRead = false;
            ReadDate = null;
        }

        public void UpdateMessage(string title, string message)
        {
            if (IsRead)
                throw new InvalidOperationException("Cannot update read notifications.");
            
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Message = message ?? throw new ArgumentNullException(nameof(message));
        }

        public void SetActionUrl(string actionUrl)
        {
            ActionUrl = actionUrl;
        }

        public void MarkAsUrgent()
        {
            IsUrgent = true;
        }

        public void RemoveUrgentStatus()
        {
            IsUrgent = false;
        }

        public void SetExpiryDate(DateTime expiryDate)
        {
            if (expiryDate <= DateTime.UtcNow)
                throw new ArgumentException("Expiry date must be in the future.");
            
            ExpiryDate = expiryDate;
        }

        public void RemoveExpiryDate()
        {
            ExpiryDate = null;
        }

        public bool IsExpired()
        {
            return ExpiryDate.HasValue && ExpiryDate.Value <= DateTime.UtcNow;
        }

        public bool IsRecent(int hours = 24)
        {
            return SentDate >= DateTime.UtcNow.AddHours(-hours);
        }

        public TimeSpan GetAge()
        {
            return DateTime.UtcNow - SentDate;
        }

        public string GetTypeDisplayName()
        {
            return Type switch
            {
                NotificationType.AppointmentConfirmation => "Appointment Confirmed",
                NotificationType.AppointmentCancellation => "Appointment Cancelled",
                NotificationType.AppointmentReminder => "Appointment Reminder",
                NotificationType.NewAppointmentRequest => "New Appointment Request",
                NotificationType.NewPrescription => "New Prescription",
                NotificationType.MedicalRecordUpdate => "Medical Record Updated",
                NotificationType.SystemAlert => "System Alert",
                NotificationType.Billing => "Billing Notice",
                NotificationType.General => "General Notice",
                _ => "Notification"
            };
        }

        public string GetPriorityLevel()
        {
            if (IsUrgent)
                return "High";
            
            return Type switch
            {
                NotificationType.SystemAlert => "High",
                NotificationType.AppointmentReminder => "Medium",
                NotificationType.NewAppointmentRequest => "Medium",
                NotificationType.Billing => "Medium",
                _ => "Low"
            };
        }

        public bool ShouldAutoExpire()
        {
            return Type switch
            {
                NotificationType.AppointmentReminder => true,
                NotificationType.SystemAlert => false,
                NotificationType.Billing => false,
                _ => true
            };
        }

        public static Notification CreateAppointmentConfirmation(int userId, string doctorName, DateTime appointmentDate)
        {
            var title = "Appointment Confirmed";
            var message = $"Your appointment with Dr. {doctorName} on {appointmentDate:MMM dd, yyyy 'at' h:mm tt} has been confirmed.";
            return new Notification(userId, title, message, NotificationType.AppointmentConfirmation);
        }

        public static Notification CreateAppointmentReminder(int userId, string doctorName, DateTime appointmentDate)
        {
            var title = "Appointment Reminder";
            var message = $"Reminder: You have an appointment with Dr. {doctorName} tomorrow at {appointmentDate:h:mm tt}.";
            return new Notification(userId, title, message, NotificationType.AppointmentReminder, isUrgent: true);
        }

        public static Notification CreateNewPrescription(int userId, string medicationName, string doctorName)
        {
            var title = "New Prescription";
            var message = $"Dr. {doctorName} has prescribed {medicationName}. Please collect it from the pharmacy.";
            return new Notification(userId, title, message, NotificationType.NewPrescription);
        }
    }
}

