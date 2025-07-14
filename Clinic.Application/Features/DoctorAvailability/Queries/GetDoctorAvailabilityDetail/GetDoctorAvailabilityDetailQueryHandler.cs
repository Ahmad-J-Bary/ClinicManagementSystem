
using AutoMapper;
using MediatR;
using Clinic.Application.Contracts.Persistence;

namespace Clinic.Application.Features.DoctorAvailability.Queries.GetDoctorAvailabilityDetail
{
    public class GetDoctorAvailabilityDetailQueryHandler : IRequestHandler<GetDoctorAvailabilityDetailQuery, DoctorAvailabilityDetailDto>
    {
        private readonly IDoctorAvailabilityRepository _doctorAvailabilityRepository;
        private readonly IMapper _mapper;

        public GetDoctorAvailabilityDetailQueryHandler(IDoctorAvailabilityRepository doctorAvailabilityRepository, IMapper mapper)
        {
            _doctorAvailabilityRepository = doctorAvailabilityRepository;
            _mapper = mapper;
        }

        public async Task<DoctorAvailabilityDetailDto> Handle(GetDoctorAvailabilityDetailQuery request, CancellationToken cancellationToken)
        {
            var doctorAvailability = await _doctorAvailabilityRepository.GetByIdAsync(request.Id);
            return _mapper.Map<DoctorAvailabilityDetailDto>(doctorAvailability);
        }
    }
}

