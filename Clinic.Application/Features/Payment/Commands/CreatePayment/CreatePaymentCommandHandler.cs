
using AutoMapper;
using MediatR;
using Clinic.Application.Contracts.Persistence;
using Clinic.Domain.Entities;

namespace Clinic.Application.Features.Payment.Commands.CreatePayment
{
    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, int>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;

        public CreatePaymentCommandHandler(IPaymentRepository paymentRepository, IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = _mapper.Map<Domain.Entities.Payment>(request);
            payment = await _paymentRepository.AddAsync(payment);
            return payment.Id;
        }
    }
}

