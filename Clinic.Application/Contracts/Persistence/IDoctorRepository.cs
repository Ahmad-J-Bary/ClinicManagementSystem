using Clinic.Domain.Entities;

namespace Clinic.Application.Contracts.Persistence
{
    /// <summary>
    /// Repository interface for Doctor entity with specific operations.
    /// </summary>
    public interface IDoctorRepository : IGenericRepository<Doctor>
    {
        Task<Doctor?> GetDoctorWithAppointmentsAsync(int doctorId);
        Task<Doctor?> GetDoctorWithAvailabilityAsync(int doctorId);
        Task<Doctor?> GetDoctorWithDepartmentAsync(int doctorId);
        Task<Doctor?> GetDoctorByLicenseNumberAsync(string licenseNumber);
        Task<IReadOnlyList<Doctor>> GetDoctorsBySpecializationAsync(string specialization);
        Task<IReadOnlyList<Doctor>> GetDoctorsByDepartmentAsync(int departmentId);
        Task<IReadOnlyList<Doctor>> GetAvailableDoctorsAsync(DateTime startTime, DateTime endTime);
        Task<IReadOnlyList<Doctor>> GetDoctorsWithTodaysAppointmentsAsync();
        Task<bool> LicenseNumberExistsAsync(string licenseNumber);
        Task<bool> IsDoctorAvailableAsync(int doctorId, DateTime startTime, DateTime endTime);
    }
}

