using MediatR;

namespace Clinic.Application.Features.Payment.Queries.GetPaymentDetail
{
    public class GetPaymentDetailQuery : IRequest<PaymentDetailDto>
    {
        public int Id { get; set; }
    }
}

