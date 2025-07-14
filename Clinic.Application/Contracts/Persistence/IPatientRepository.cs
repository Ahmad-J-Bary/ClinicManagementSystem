using Clinic.Domain.Entities;

namespace Clinic.Application.Contracts.Persistence
{
    /// <summary>
    /// Repository interface for Patient entity operations.
    /// Extends the generic repository with patient-specific methods.
    /// </summary>
    public interface IPatientRepository : IGenericRepository<Patient>
    {
        /// <summary>
        /// Retrieves a patient by their unique patient ID number.
        /// </summary>
        /// <param name="patientIdNumber">The patient's unique ID number</param>
        /// <returns>The patient if found, null otherwise</returns>
        Task<Patient?> GetPatientByIdNumberAsync(string patientIdNumber);

        /// <summary>
        /// Retrieves a patient by their email address.
        /// </summary>
        /// <param name="email">The patient's email address</param>
        /// <returns>The patient if found, null otherwise</returns>
        Task<Patient?> GetPatientByEmailAsync(string email);

        /// <summary>
        /// Retrieves patients with upcoming appointments.
        /// </summary>
        /// <param name="fromDate">Start date for appointment search</param>
        /// <param name="toDate">End date for appointment search</param>
        /// <returns>List of patients with upcoming appointments</returns>
        Task<IReadOnlyList<Patient>> GetPatientsWithUpcomingAppointmentsAsync(DateTime fromDate, DateTime toDate);

        /// <summary>
        /// Retrieves patients with outstanding payments.
        /// </summary>
        /// <returns>List of patients with pending payments</returns>
        Task<IReadOnlyList<Patient>> GetPatientsWithOutstandingPaymentsAsync();

        /// <summary>
        /// Retrieves a patient with their complete medical history.
        /// </summary>
        /// <param name="id">The patient's ID</param>
        /// <returns>Patient with medical records, appointments, and payments</returns>
        Task<Patient?> GetPatientWithCompleteHistoryAsync(int id);

        /// <summary>
        /// Searches patients by name, email, or patient ID number.
        /// </summary>
        /// <param name="searchTerm">The search term</param>
        /// <returns>List of matching patients</returns>
        Task<IReadOnlyList<Patient>> SearchPatientsAsync(string searchTerm);

        /// <summary>
        /// Gets patients by insurance provider.
        /// </summary>
        /// <param name="insuranceProvider">The insurance provider name</param>
        /// <returns>List of patients with the specified insurance provider</returns>
        Task<IReadOnlyList<Patient>> GetPatientsByInsuranceProviderAsync(string insuranceProvider);
    }
}

