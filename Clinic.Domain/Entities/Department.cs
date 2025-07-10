using System;
using System.Collections.Generic;
using System.Linq;

namespace Clinic.Domain.Entities
{
    /// <summary>
    /// Represents a medical department within the clinic (e.g., Cardiology, Pediatrics).
    /// This entity helps organize doctors and services.
    /// </summary>
    public class Department : BaseEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string? HeadOfDepartment { get; private set; }
        public string? Location { get; private set; }
        public string? PhoneNumber { get; private set; }
        public bool IsActive { get; private set; }

        // Navigation properties
        public virtual ICollection<Doctor> Doctors { get; private set; } = new List<Doctor>();

        private Department() { }

        public Department(string name, string description, string? headOfDepartment = null, 
                         string? location = null, string? phoneNumber = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            HeadOfDepartment = headOfDepartment;
            Location = location;
            PhoneNumber = phoneNumber;
            IsActive = true;
        }

        public void UpdateInfo(string name, string description)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }

        public void UpdateContactInfo(string? headOfDepartment, string? location, string? phoneNumber)
        {
            HeadOfDepartment = headOfDepartment;
            Location = location;
            PhoneNumber = phoneNumber;
        }

        public void AddDoctor(Doctor doctor)
        {
            if (doctor == null) throw new ArgumentNullException(nameof(doctor));
            
            if (Doctors.Any(d => d.Id == doctor.Id))
                throw new InvalidOperationException("Doctor is already assigned to this department.");
            
            Doctors.Add(doctor);
        }

        public void RemoveDoctor(Doctor doctor)
        {
            if (doctor == null) throw new ArgumentNullException(nameof(doctor));
            
            var existingDoctor = Doctors.FirstOrDefault(d => d.Id == doctor.Id);
            if (existingDoctor == null)
                throw new InvalidOperationException("Doctor is not assigned to this department.");
            
            Doctors.Remove(existingDoctor);
        }

        public void Activate()
        {
            IsActive = true;
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        public IEnumerable<Doctor> GetActiveDoctors()
        {
            return Doctors.Where(d => d.IsActive);
        }

        public IEnumerable<Doctor> GetDoctorsBySpecialization(string specialization)
        {
            if (string.IsNullOrWhiteSpace(specialization))
                throw new ArgumentException("Specialization cannot be null or empty.", nameof(specialization));
            
            return Doctors.Where(d => d.IsActive && 
                                    d.Specialization.Equals(specialization, StringComparison.OrdinalIgnoreCase));
        }

        public int GetDoctorCount()
        {
            return Doctors.Count(d => d.IsActive);
        }

        public bool HasDoctors()
        {
            return Doctors.Any(d => d.IsActive);
        }

        public Doctor? GetHeadDoctor()
        {
            if (string.IsNullOrWhiteSpace(HeadOfDepartment))
                return null;
            
            return Doctors.FirstOrDefault(d => d.IsActive && 
                                             d.GetFullName().Equals(HeadOfDepartment, StringComparison.OrdinalIgnoreCase));
        }
    }
}

