
using AutoMapper;
using MediatR;
using Clinic.Application.Contracts.Persistence;
using Clinic.Domain.Entities;

namespace Clinic.Application.Features.Receptionist.Commands.CreateReceptionist
{
    public class CreateReceptionistCommandHandler : IRequestHandler<CreateReceptionistCommand, int>
    {
        private readonly IReceptionistRepository _receptionistRepository;
        private readonly IMapper _mapper;

        public CreateReceptionistCommandHandler(IReceptionistRepository receptionistRepository, IMapper mapper)
        {
            _receptionistRepository = receptionistRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateReceptionistCommand request, CancellationToken cancellationToken)
        {
            var receptionist = _mapper.Map<Domain.Entities.Receptionist>(request);
            receptionist = await _receptionistRepository.AddAsync(receptionist);
            return receptionist.Id;
        }
    }
}

