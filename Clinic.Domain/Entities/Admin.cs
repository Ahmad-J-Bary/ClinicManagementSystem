using System;
using System.Collections.Generic;
using Clinic.Domain.Enums;

namespace Clinic.Domain.Entities
{
    /// <summary>
    /// Represents an administrator of the clinic management system.
    /// This role typically has elevated privileges for managing users, configurations, and reports.
    /// </summary>
    public class Admin : User
    {
        public string? EmployeeId { get; private set; }
        public DateTime? LastLoginDate { get; private set; }
        public string? AccessLevel { get; private set; } // Super Admin, Admin, etc.

        private Admin() : base() { }

        public Admin(string firstName, string lastName, string email, string phoneNumber, 
                    string address, DateTime dateOfBirth, string identityUserId, 
                    string? employeeId = null, string? accessLevel = null)
            : base(firstName, lastName, email, phoneNumber, address, dateOfBirth, identityUserId, UserRole.Admin)
        {
            EmployeeId = employeeId;
            AccessLevel = accessLevel ?? "Admin";
        }

        public void UpdateEmployeeInfo(string? employeeId, string? accessLevel)
        {
            EmployeeId = employeeId;
            AccessLevel = accessLevel;
        }

        public void RecordLogin()
        {
            LastLoginDate = DateTime.UtcNow;
        }

        public void DeactivateUser(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            user.Deactivate();
        }

        public void ActivateUser(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            user.Activate();
        }

        public void ConfigureDepartment(Department department, string name, string description)
        {
            if (department == null) throw new ArgumentNullException(nameof(department));
            department.UpdateInfo(name, description);
        }

        public bool CanManageUsers()
        {
            return AccessLevel == "Super Admin" || AccessLevel == "Admin";
        }

        public bool CanViewReports()
        {
            return AccessLevel == "Super Admin" || AccessLevel == "Admin";
        }

        public bool CanManageSystem()
        {
            return AccessLevel == "Super Admin";
        }
    }
}

