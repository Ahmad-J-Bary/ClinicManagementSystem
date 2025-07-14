
using AutoMapper;
using MediatR;
using Clinic.Application.Contracts.Persistence;

namespace Clinic.Application.Features.Pharmacist.Queries.GetPharmacistDetail
{
    public class GetPharmacistDetailQueryHandler : IRequestHandler<GetPharmacistDetailQuery, PharmacistDetailDto>
    {
        private readonly IPharmacistRepository _pharmacistRepository;
        private readonly IMapper _mapper;

        public GetPharmacistDetailQueryHandler(IPharmacistRepository pharmacistRepository, IMapper mapper)
        {
            _pharmacistRepository = pharmacistRepository;
            _mapper = mapper;
        }

        public async Task<PharmacistDetailDto> Handle(GetPharmacistDetailQuery request, CancellationToken cancellationToken)
        {
            var pharmacist = await _pharmacistRepository.GetByIdAsync(request.Id);
            return _mapper.Map<PharmacistDetailDto>(pharmacist);
        }
    }
}

