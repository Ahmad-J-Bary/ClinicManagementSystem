using Clinic.Domain.Entities;
using Clinic.Domain.Enums;

namespace Clinic.Application.Contracts.Persistence
{
    /// <summary>
    /// Repository interface for Appointment entity with specific operations.
    /// </summary>
    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        Task<Appointment?> GetAppointmentWithDetailsAsync(int appointmentId);
        Task<IReadOnlyList<Appointment>> GetAppointmentsByPatientAsync(int patientId);
        Task<IReadOnlyList<Appointment>> GetAppointmentsByDoctorAsync(int doctorId);
        Task<IReadOnlyList<Appointment>> GetAppointmentsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IReadOnlyList<Appointment>> GetAppointmentsByStatusAsync(AppointmentStatus status);
        Task<IReadOnlyList<Appointment>> GetTodaysAppointmentsAsync();
        Task<IReadOnlyList<Appointment>> GetUpcomingAppointmentsAsync(int days = 7);
        Task<IReadOnlyList<Appointment>> GetOverdueAppointmentsAsync();
        Task<IReadOnlyList<Appointment>> GetAppointmentsByDoctorAndDateAsync(int doctorId, DateTime date);
        Task<bool> HasConflictingAppointmentAsync(int doctorId, DateTime startTime, DateTime endTime, int? excludeAppointmentId = null);
        Task<int> GetAppointmentCountByStatusAsync(AppointmentStatus status);
    }
}

