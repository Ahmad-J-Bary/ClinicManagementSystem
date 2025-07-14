using Clinic.Application.Models.Email;

namespace Clinic.Application.Contracts.Infrastructure
{
    /// <summary>
    /// Interface for email service operations.
    /// Defines methods for sending various types of emails in the clinic system.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Sends an email message.
        /// </summary>
        /// <param name="email">The email message to send</param>
        /// <returns>True if email was sent successfully, false otherwise</returns>
        Task<bool> SendEmailAsync(EmailMessage email);

        /// <summary>
        /// Sends an appointment confirmation email to a patient.
        /// </summary>
        /// <param name="patientEmail">Patient's email address</param>
        /// <param name="patientName">Patient's full name</param>
        /// <param name="appointmentDate">Appointment date and time</param>
        /// <param name="doctorName">Doctor's name</param>
        /// <param name="clinicAddress">Clinic address</param>
        /// <returns>True if email was sent successfully, false otherwise</returns>
        Task<bool> SendAppointmentConfirmationAsync(string patientEmail, string patientName, 
            DateTime appointmentDate, string doctorName, string clinicAddress);

        /// <summary>
        /// Sends an appointment reminder email to a patient.
        /// </summary>
        /// <param name="patientEmail">Patient's email address</param>
        /// <param name="patientName">Patient's full name</param>
        /// <param name="appointmentDate">Appointment date and time</param>
        /// <param name="doctorName">Doctor's name</param>
        /// <returns>True if email was sent successfully, false otherwise</returns>
        Task<bool> SendAppointmentReminderAsync(string patientEmail, string patientName, 
            DateTime appointmentDate, string doctorName);

        /// <summary>
        /// Sends an appointment cancellation email to a patient.
        /// </summary>
        /// <param name="patientEmail">Patient's email address</param>
        /// <param name="patientName">Patient's full name</param>
        /// <param name="appointmentDate">Cancelled appointment date and time</param>
        /// <param name="reason">Reason for cancellation</param>
        /// <returns>True if email was sent successfully, false otherwise</returns>
        Task<bool> SendAppointmentCancellationAsync(string patientEmail, string patientName, 
            DateTime appointmentDate, string reason);

        /// <summary>
        /// Sends a welcome email to a new patient.
        /// </summary>
        /// <param name="patientEmail">Patient's email address</param>
        /// <param name="patientName">Patient's full name</param>
        /// <param name="patientId">Patient's ID number</param>
        /// <returns>True if email was sent successfully, false otherwise</returns>
        Task<bool> SendWelcomeEmailAsync(string patientEmail, string patientName, string patientId);

        /// <summary>
        /// Sends a prescription ready notification email to a patient.
        /// </summary>
        /// <param name="patientEmail">Patient's email address</param>
        /// <param name="patientName">Patient's full name</param>
        /// <param name="prescriptionDetails">Prescription details</param>
        /// <param name="pharmacyInfo">Pharmacy information</param>
        /// <returns>True if email was sent successfully, false otherwise</returns>
        Task<bool> SendPrescriptionReadyAsync(string patientEmail, string patientName, 
            string prescriptionDetails, string pharmacyInfo);
    }
}

