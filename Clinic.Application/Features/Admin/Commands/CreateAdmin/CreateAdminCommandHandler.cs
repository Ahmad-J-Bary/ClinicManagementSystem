using AutoMapper;
using MediatR;
using Clinic.Application.Contracts.Persistence;
using Clinic.Domain.Entities;

namespace Clinic.Application.Features.Admin.Commands.CreateAdmin
{
    public class CreateAdminCommandHandler : IRequestHandler<CreateAdminCommand, int>
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper _mapper;

        public CreateAdminCommandHandler(IAdminRepository adminRepository, IMapper mapper)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
        {
            var admin = _mapper.Map<Domain.Entities.Admin>(request);
            admin = await _adminRepository.AddAsync(admin);
            return admin.Id;
        }
    }
}

