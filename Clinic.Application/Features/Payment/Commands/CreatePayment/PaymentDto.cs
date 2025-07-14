
using Clinic.Domain.Enums;

namespace Clinic.Application.Features.Payment.Commands.CreatePayment
{
    public class PaymentDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentStatus Status { get; set; }
        public int? AppointmentId { get; set; }
    }
}

