using Clinic.Domain.Entities;

namespace Clinic.Application.Contracts.Persistence
{
    /// <summary>
    /// Repository interface for Patient entity with specific operations.
    /// </summary>
    public interface IPatientRepository : IGenericRepository<Patient>
    {
        Task<Patient?> GetPatientWithAppointmentsAsync(int patientId);
        Task<Patient?> GetPatientWithMedicalRecordsAsync(int patientId);
        Task<Patient?> GetPatientByPatientIdNumberAsync(string patientIdNumber);
        Task<Patient?> GetPatientByEmailAsync(string email);
        Task<IReadOnlyList<Patient>> GetPatientsByInsuranceProviderAsync(string insuranceProvider);
        Task<IReadOnlyList<Patient>> GetPatientsWithUpcomingAppointmentsAsync();
        Task<bool> PatientIdNumberExistsAsync(string patientIdNumber);
        Task<decimal> GetPatientOutstandingBalanceAsync(int patientId);
    }
}

