namespace Clinic.Domain.Enums
{
    /// <summary>
    /// Represents the type of notification in the clinic management system.
    /// </summary>
    public enum NotificationType
    {
        /// <summary>
        /// Notification about appointment confirmation.
        /// </summary>
        AppointmentConfirmation = 0,

        /// <summary>
        /// Notification about appointment cancellation.
        /// </summary>
        AppointmentCancellation = 1,

        /// <summary>
        /// Reminder notification for upcoming appointment.
        /// </summary>
        AppointmentReminder = 2,

        /// <summary>
        /// Notification about new appointment request.
        /// </summary>
        NewAppointmentRequest = 3,

        /// <summary>
        /// Notification about new prescription issued.
        /// </summary>
        NewPrescription = 4,

        /// <summary>
        /// Notification about medical record update.
        /// </summary>
        MedicalRecordUpdate = 5,

        /// <summary>
        /// System alert notification.
        /// </summary>
        SystemAlert = 6,

        /// <summary>
        /// Billing and payment related notification.
        /// </summary>
        Billing = 7,

        /// <summary>
        /// General notification type.
        /// </summary>
        General = 8
    }
}

