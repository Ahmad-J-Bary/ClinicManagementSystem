using System;

namespace Clinic.Domain
{
    /// <summary>
    /// Abstract base class that provides common properties for all domain entities.
    /// This promotes consistency and ensures that all entities have basic auditing information.
    /// </summary>
    public abstract class BaseEntity
    {
        public int Id { get; protected set; }
        public DateTime DateCreated { get; protected set; }
        public string CreatedBy { get; protected set; }
        public DateTime? DateModified { get; protected set; }
        public string? ModifiedBy { get; protected set; }

        protected BaseEntity()
        {
            DateCreated = DateTime.UtcNow;
        }

        public void SetCreatedBy(string createdBy)
        {
            CreatedBy = createdBy;
        }

        public void SetModifiedBy(string modifiedBy)
        {
            DateModified = DateTime.UtcNow;
            ModifiedBy = modifiedBy;
        }
    }
}

