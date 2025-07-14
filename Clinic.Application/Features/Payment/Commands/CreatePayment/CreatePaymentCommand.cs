using MediatR;
using Clinic.Domain.Enums;

namespace Clinic.Application.Features.Payment.Commands.CreatePayment
{
    public class CreatePaymentCommand : IRequest<int>
    {
        public int PatientId { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentStatus Status { get; set; }
        public int? AppointmentId { get; set; }
    }
}

