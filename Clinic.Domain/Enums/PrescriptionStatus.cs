namespace Clinic.Domain.Enums
{
    /// <summary>
    /// Represents the status of a prescription in the clinic management system.
    /// </summary>
    public enum PrescriptionStatus
    {
        /// <summary>
        /// Prescription is active and can be dispensed.
        /// </summary>
        Active = 0,

        /// <summary>
        /// Prescription has been dispensed by the pharmacist.
        /// </summary>
        Dispensed = 1,

        /// <summary>
        /// Prescription has been cancelled and cannot be dispensed.
        /// </summary>
        Cancelled = 2,

        /// <summary>
        /// Prescription has expired and is no longer valid.
        /// </summary>
        Expired = 3
    }
}

