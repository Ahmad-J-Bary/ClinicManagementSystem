namespace Clinic.Domain.Enums
{
    /// <summary>
    /// Represents the payment method used for clinic services.
    /// </summary>
    public enum PaymentMethod
    {
        /// <summary>
        /// Payment made with credit card.
        /// </summary>
        CreditCard = 0,

        /// <summary>
        /// Payment made with debit card.
        /// </summary>
        DebitCard = 1,

        /// <summary>
        /// Cash payment.
        /// </summary>
        Cash = 2,

        /// <summary>
        /// Payment covered by insurance.
        /// </summary>
        Insurance = 3,

        /// <summary>
        /// Bank transfer payment.
        /// </summary>
        BankTransfer = 4,

        /// <summary>
        /// Digital wallet payment (PayPal, Apple Pay, etc.).
        /// </summary>
        DigitalWallet = 5,

        /// <summary>
        /// Other payment method.
        /// </summary>
        Other = 6
    }
}

