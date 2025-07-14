
using AutoMapper;
using MediatR;
using Clinic.Application.Contracts.Persistence;
using Clinic.Domain.Entities;

namespace Clinic.Application.Features.DoctorAvailability.Commands.CreateDoctorAvailability
{
    public class CreateDoctorAvailabilityCommandHandler : IRequestHandler<CreateDoctorAvailabilityCommand, int>
    {
        private readonly IDoctorAvailabilityRepository _doctorAvailabilityRepository;
        private readonly IMapper _mapper;

        public CreateDoctorAvailabilityCommandHandler(IDoctorAvailabilityRepository doctorAvailabilityRepository, IMapper mapper)
        {
            _doctorAvailabilityRepository = doctorAvailabilityRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateDoctorAvailabilityCommand request, CancellationToken cancellationToken)
        {
            var doctorAvailability = _mapper.Map<Domain.Entities.DoctorAvailability>(request);
            doctorAvailability = await _doctorAvailabilityRepository.AddAsync(doctorAvailability);
            return doctorAvailability.Id;
        }
    }
}

