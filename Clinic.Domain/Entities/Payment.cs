using System;
using Clinic.Domain.Enums;

namespace Clinic.Domain.Entities
{
    /// <summary>
    /// Represents a payment made by a patient for services.
    /// This entity would typically integrate with a payment gateway.
    /// </summary>
    public class Payment : BaseEntity
    {
        public decimal Amount { get; private set; }
        public DateTime PaymentDate { get; private set; }
        public PaymentMethod Method { get; private set; }
        public string? TransactionId { get; private set; }
        public PaymentStatus Status { get; private set; }
        public string? Description { get; private set; }
        public string? ReceiptNumber { get; private set; }
        public decimal? RefundAmount { get; private set; }
        public DateTime? RefundDate { get; private set; }
        public string? RefundReason { get; private set; }
        public string? PaymentGatewayResponse { get; private set; }
        public string? FailureReason { get; private set; }

        // Foreign keys
        public int PatientId { get; private set; }
        public int? AppointmentId { get; private set; }

        // Navigation properties
        public virtual Patient Patient { get; private set; }
        public virtual Appointment? Appointment { get; private set; }

        private Payment() { }

        public Payment(int patientId, decimal amount, PaymentMethod method, 
                      string? description = null, int? appointmentId = null, string? transactionId = null)
        {
            PatientId = patientId;
            Amount = amount > 0 ? amount : throw new ArgumentException("Amount must be greater than zero.");
            PaymentDate = DateTime.UtcNow;
            Method = method;
            Description = description;
            AppointmentId = appointmentId;
            TransactionId = transactionId;
            Status = PaymentStatus.Pending;
            ReceiptNumber = GenerateReceiptNumber();
        }

        public void MarkAsCompleted(string? transactionId = null, string? gatewayResponse = null)
        {
            if (Status != PaymentStatus.Pending && Status != PaymentStatus.Processing)
                throw new InvalidOperationException("Only pending or processing payments can be completed.");
            
            Status = PaymentStatus.Completed;
            
            if (!string.IsNullOrWhiteSpace(transactionId))
                TransactionId = transactionId;
            
            PaymentGatewayResponse = gatewayResponse;
        }

        public void MarkAsFailed(string reason, string? gatewayResponse = null)
        {
            if (Status == PaymentStatus.Completed)
                throw new InvalidOperationException("Completed payments cannot be marked as failed.");
            
            if (Status == PaymentStatus.Refunded)
                throw new InvalidOperationException("Refunded payments cannot be marked as failed.");
            
            Status = PaymentStatus.Failed;
            FailureReason = reason ?? throw new ArgumentNullException(nameof(reason));
            PaymentGatewayResponse = gatewayResponse;
        }

        public void MarkAsProcessing()
        {
            if (Status != PaymentStatus.Pending)
                throw new InvalidOperationException("Only pending payments can be marked as processing.");
            
            Status = PaymentStatus.Processing;
        }

        public void Cancel(string reason)
        {
            if (Status == PaymentStatus.Completed)
                throw new InvalidOperationException("Completed payments cannot be cancelled.");
            
            if (Status == PaymentStatus.Refunded)
                throw new InvalidOperationException("Refunded payments cannot be cancelled.");
            
            Status = PaymentStatus.Cancelled;
            FailureReason = reason ?? throw new ArgumentNullException(nameof(reason));
        }

        public void ProcessRefund(decimal refundAmount, string reason)
        {
            if (Status != PaymentStatus.Completed)
                throw new InvalidOperationException("Only completed payments can be refunded.");
            
            if (refundAmount <= 0)
                throw new ArgumentException("Refund amount must be greater than zero.");
            
            if (refundAmount > Amount)
                throw new ArgumentException("Refund amount cannot exceed the original payment amount.");
            
            if (RefundAmount.HasValue && RefundAmount.Value + refundAmount > Amount)
                throw new ArgumentException("Total refund amount cannot exceed the original payment amount.");
            
            RefundAmount = (RefundAmount ?? 0) + refundAmount;
            RefundDate = DateTime.UtcNow;
            RefundReason = reason ?? throw new ArgumentNullException(nameof(reason));
            
            // If fully refunded, update status
            if (RefundAmount >= Amount)
                Status = PaymentStatus.Refunded;
        }

        public void UpdateTransactionId(string transactionId)
        {
            TransactionId = transactionId;
        }

        public void UpdateDescription(string description)
        {
            Description = description;
        }

        public void UpdateGatewayResponse(string gatewayResponse)
        {
            PaymentGatewayResponse = gatewayResponse;
        }

        public bool IsSuccessful()
        {
            return Status == PaymentStatus.Completed;
        }

        public bool IsPending()
        {
            return Status == PaymentStatus.Pending || Status == PaymentStatus.Processing;
        }

        public bool CanBeRefunded()
        {
            return Status == PaymentStatus.Completed && 
                   (!RefundAmount.HasValue || RefundAmount.Value < Amount);
        }

        public decimal GetRemainingRefundableAmount()
        {
            if (!CanBeRefunded())
                return 0;
            
            return Amount - (RefundAmount ?? 0);
        }

        public bool IsPartiallyRefunded()
        {
            return RefundAmount.HasValue && RefundAmount.Value > 0 && RefundAmount.Value < Amount;
        }

        public bool IsFullyRefunded()
        {
            return Status == PaymentStatus.Refunded || 
                   (RefundAmount.HasValue && RefundAmount.Value >= Amount);
        }

        public string GetPaymentMethodDisplayName()
        {
            return Method switch
            {
                PaymentMethod.CreditCard => "Credit Card",
                PaymentMethod.DebitCard => "Debit Card",
                PaymentMethod.Cash => "Cash",
                PaymentMethod.Insurance => "Insurance",
                PaymentMethod.BankTransfer => "Bank Transfer",
                PaymentMethod.DigitalWallet => "Digital Wallet",
                PaymentMethod.Other => "Other",
                _ => "Unknown"
            };
        }

        public string GetStatusDisplayName()
        {
            return Status switch
            {
                PaymentStatus.Pending => "Pending",
                PaymentStatus.Processing => "Processing",
                PaymentStatus.Completed => "Completed",
                PaymentStatus.Failed => "Failed",
                PaymentStatus.Refunded => "Refunded",
                PaymentStatus.Cancelled => "Cancelled",
                _ => "Unknown"
            };
        }

        private string GenerateReceiptNumber()
        {
            return $"RCP-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString("N")[..8].ToUpper()}";
        }

        public static Payment CreateAppointmentPayment(int patientId, int appointmentId, decimal amount, PaymentMethod method)
        {
            return new Payment(patientId, amount, method, "Appointment consultation fee", appointmentId);
        }

        public static Payment CreateGeneralPayment(int patientId, decimal amount, PaymentMethod method, string description)
        {
            return new Payment(patientId, amount, method, description);
        }
    }
}

