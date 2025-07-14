using Clinic.Domain.Enums;

namespace Clinic.Application.Features.Payment.Queries.GetPaymentDetail
{
    public class PaymentDetailDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentStatus Status { get; set; }
        public int? AppointmentId { get; set; }
    }
}

