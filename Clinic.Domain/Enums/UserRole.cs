namespace Clinic.Domain.Enums
{
    /// <summary>
    /// Represents the role of a user in the clinic management system.
    /// </summary>
    public enum UserRole
    {
        /// <summary>
        /// Patient role - can book appointments, view medical records.
        /// </summary>
        Patient = 0,

        /// <summary>
        /// Doctor role - can manage appointments, create medical records.
        /// </summary>
        Doctor = 1,

        /// <summary>
        /// Administrator role - can manage all system aspects.
        /// </summary>
        Admin = 2,

        /// <summary>
        /// Receptionist role - can manage appointments and patient check-ins.
        /// </summary>
        Receptionist = 3,

        /// <summary>
        /// Pharmacist role - can manage prescriptions and medications.
        /// </summary>
        Pharmacist = 4
    }
}

