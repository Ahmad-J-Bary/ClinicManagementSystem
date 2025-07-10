using System;
using System.Collections.Generic;
using Clinic.Domain.Enums;

namespace Clinic.Domain.Entities
{
    /// <summary>
    /// Abstract base class representing a generic user in the clinic management system.
    /// This will be inherited by specific user roles like Patient, Doctor, Admin, etc.
    /// </summary>
    public abstract class User : BaseEntity
    {
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string Email { get; protected set; }
        public string PhoneNumber { get; protected set; }
        public string Address { get; protected set; }
        public DateTime DateOfBirth { get; protected set; }
        public UserRole Role { get; protected set; }
        public bool IsActive { get; protected set; }

        // This would likely be managed by ASP.NET Core Identity
        public string IdentityUserId { get; protected set; }

        // Navigation properties
        public virtual ICollection<Notification> Notifications { get; protected set; } = new List<Notification>();

        protected User() { }

        protected User(string firstName, string lastName, string email, string phoneNumber, 
                      string address, DateTime dateOfBirth, string identityUserId, UserRole role)
        {
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
            Address = address ?? throw new ArgumentNullException(nameof(address));
            DateOfBirth = dateOfBirth;
            IdentityUserId = identityUserId ?? throw new ArgumentNullException(nameof(identityUserId));
            Role = role;
            IsActive = true;
        }

        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }

        public void UpdateProfile(string firstName, string lastName, string phoneNumber, string address)
        {
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
            Address = address ?? throw new ArgumentNullException(nameof(address));
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        public void Activate()
        {
            IsActive = true;
        }

        public void AddNotification(Notification notification)
        {
            if (notification == null) throw new ArgumentNullException(nameof(notification));
            Notifications.Add(notification);
        }
    }
}

