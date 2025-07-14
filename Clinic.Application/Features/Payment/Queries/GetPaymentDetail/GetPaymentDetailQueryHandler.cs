
using AutoMapper;
using MediatR;
using Clinic.Application.Contracts.Persistence;

namespace Clinic.Application.Features.Payment.Queries.GetPaymentDetail
{
    public class GetPaymentDetailQueryHandler : IRequestHandler<GetPaymentDetailQuery, PaymentDetailDto>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;

        public GetPaymentDetailQueryHandler(IPaymentRepository paymentRepository, IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }

        public async Task<PaymentDetailDto> Handle(GetPaymentDetailQuery request, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.GetByIdAsync(request.Id);
            return _mapper.Map<PaymentDetailDto>(payment);
        }
    }
}

