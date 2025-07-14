
using AutoMapper;
using MediatR;
using Clinic.Application.Contracts.Persistence;
using Clinic.Domain.Entities;

namespace Clinic.Application.Features.Admin.Commands.UpdateAdmin
{
    public class UpdateAdminCommandHandler : IRequestHandler<UpdateAdminCommand>
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper _mapper;

        public UpdateAdminCommandHandler(IAdminRepository adminRepository, IMapper mapper)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateAdminCommand request, CancellationToken cancellationToken)
        {
            var admin = await _adminRepository.GetByIdAsync(request.Id);
            _mapper.Map(request, admin);
            await _adminRepository.UpdateAsync(admin);
            return Unit.Value;
        }
    }
}

