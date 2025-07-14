using Clinic.Application.Contracts.Persistence;
using Clinic.Domain.Entities;
using Clinic.Domain.Enums;
using Clinic.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Persistence.Repositories
{
    /// <summary>
    /// Repository implementation for Patient entity operations.
    /// Extends the generic repository with patient-specific methods.
    /// </summary>
    public class PatientRepository : GenericRepository<Patient>, IPatientRepository
    {
        public PatientRepository(ClinicDbContext context) : base(context)
        {
        }

        public async Task<Patient?> GetPatientByIdNumberAsync(string patientIdNumber)
        {
            return await _dbSet
                .FirstOrDefaultAsync(p => p.PatientIdNumber == patientIdNumber);
        }

        public async Task<Patient?> GetPatientByEmailAsync(string email)
        {
            return await _dbSet
                .FirstOrDefaultAsync(p => p.Email == email);
        }

        public async Task<IReadOnlyList<Patient>> GetPatientsWithUpcomingAppointmentsAsync(DateTime fromDate, DateTime toDate)
        {
            return await _dbSet
                .Include(p => p.Appointments)
                .Where(p => p.Appointments.Any(a => 
                    a.StartTime >= fromDate && 
                    a.StartTime <= toDate && 
                    (a.Status == AppointmentStatus.Confirmed || a.Status == AppointmentStatus.Pending)))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Patient>> GetPatientsWithOutstandingPaymentsAsync()
        {
            return await _dbSet
                .Include(p => p.Payments)
                .Where(p => p.Payments.Any(payment => payment.Status == PaymentStatus.Pending))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Patient?> GetPatientWithCompleteHistoryAsync(int id)
        {
            return await _dbSet
                .Include(p => p.MedicalRecords)
                    .ThenInclude(mr => mr.Prescriptions)
                .Include(p => p.Appointments)
                    .ThenInclude(a => a.Doctor)
                .Include(p => p.Payments)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyList<Patient>> SearchPatientsAsync(string searchTerm)
        {
            var lowerSearchTerm = searchTerm.ToLower();
            
            return await _dbSet
                .Where(p => 
                    p.FirstName.ToLower().Contains(lowerSearchTerm) ||
                    p.LastName.ToLower().Contains(lowerSearchTerm) ||
                    p.Email.ToLower().Contains(lowerSearchTerm) ||
                    p.PatientIdNumber.ToLower().Contains(lowerSearchTerm))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Patient>> GetPatientsByInsuranceProviderAsync(string insuranceProvider)
        {
            return await _dbSet
                .Where(p => p.InsuranceProvider != null && 
                           p.InsuranceProvider.ToLower() == insuranceProvider.ToLower())
                .AsNoTracking()
                .ToListAsync();
        }
    }
}

