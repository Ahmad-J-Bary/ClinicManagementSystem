
using AutoMapper;
using MediatR;
using Clinic.Application.Contracts.Persistence;
using Clinic.Domain.Entities;

namespace Clinic.Application.Features.Pharmacist.Commands.CreatePharmacist
{
    public class CreatePharmacistCommandHandler : IRequestHandler<CreatePharmacistCommand, int>
    {
        private readonly IPharmacistRepository _pharmacistRepository;
        private readonly IMapper _mapper;

        public CreatePharmacistCommandHandler(IPharmacistRepository pharmacistRepository, IMapper mapper)
        {
            _pharmacistRepository = pharmacistRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreatePharmacistCommand request, CancellationToken cancellationToken)
        {
            var pharmacist = _mapper.Map<Domain.Entities.Pharmacist>(request);
            pharmacist = await _pharmacistRepository.AddAsync(pharmacist);
            return pharmacist.Id;
        }
    }
}

