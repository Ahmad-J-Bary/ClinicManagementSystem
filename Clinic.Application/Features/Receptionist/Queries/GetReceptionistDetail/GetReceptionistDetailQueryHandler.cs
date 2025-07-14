
using AutoMapper;
using MediatR;
using Clinic.Application.Contracts.Persistence;

namespace Clinic.Application.Features.Receptionist.Queries.GetReceptionistDetail
{
    public class GetReceptionistDetailQueryHandler : IRequestHandler<GetReceptionistDetailQuery, ReceptionistDetailDto>
    {
        private readonly IReceptionistRepository _receptionistRepository;
        private readonly IMapper _mapper;

        public GetReceptionistDetailQueryHandler(IReceptionistRepository receptionistRepository, IMapper mapper)
        {
            _receptionistRepository = receptionistRepository;
            _mapper = mapper;
        }

        public async Task<ReceptionistDetailDto> Handle(GetReceptionistDetailQuery request, CancellationToken cancellationToken)
        {
            var receptionist = await _receptionistRepository.GetByIdAsync(request.Id);
            return _mapper.Map<ReceptionistDetailDto>(receptionist);
        }
    }
}

