using Clinic.Domain;
using Clinic.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Persistence.DatabaseContext
{
    /// <summary>
    /// Entity Framework DbContext for the Clinic Management System.
    /// Defines the database schema and entity relationships.
    /// </summary>
    public class ClinicDbContext : DbContext
    {
        public ClinicDbContext(DbContextOptions<ClinicDbContext> options) : base(options)
        {
        }

        // DbSets for all entities
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Receptionist> Receptionists { get; set; }
        public DbSet<Pharmacist> Pharmacists { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<DoctorAvailability> DoctorAvailabilities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply all entity configurations from the current assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClinicDbContext).Assembly);

            // Configure inheritance for User entity
            modelBuilder.Entity<User>()
                .HasDiscriminator<string>("UserType")
                .HasValue<Patient>("Patient")
                .HasValue<Doctor>("Doctor")
                .HasValue<Admin>("Admin")
                .HasValue<Receptionist>("Receptionist")
                .HasValue<Pharmacist>("Pharmacist");

            // Configure common properties for all entities
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // Configure CreatedDate and LastModifiedDate for all entities that inherit from BaseEntity
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .Property("CreatedDate")
                        .HasDefaultValueSql("GETUTCDATE()");
                }
            }
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Automatically set LastModifiedDate for modified entities
            foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
                .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
            {
                entry.Entity.LastModifiedDate = DateTime.UtcNow;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}

