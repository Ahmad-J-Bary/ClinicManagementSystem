namespace Clinic.Domain.Enums
{
    /// <summary>
    /// Represents the status of an appointment in the clinic management system.
    /// </summary>
    public enum AppointmentStatus
    {
        /// <summary>
        /// Appointment has been requested but not yet confirmed by the doctor.
        /// </summary>
        Pending = 0,

        /// <summary>
        /// Appointment has been confirmed by the doctor.
        /// </summary>
        Confirmed = 1,

        /// <summary>
        /// Appointment has been cancelled by either patient or doctor.
        /// </summary>
        Cancelled = 2,

        /// <summary>
        /// Appointment has been rejected by the doctor.
        /// </summary>
        Rejected = 3,

        /// <summary>
        /// Appointment has been completed successfully.
        /// </summary>
        Completed = 4,

        /// <summary>
        /// Patient did not show up for the appointment.
        /// </summary>
        NoShow = 5
    }
}

