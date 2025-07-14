using AutoMapper;
using MediatR;
using Clinic.Application.Contracts.Persistence;

namespace Clinic.Application.Features.Admin.Queries.GetAdminDetail
{
    public class GetAdminDetailQueryHandler : IRequestHandler<GetAdminDetailQuery, AdminDetailDto>
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper _mapper;

        public GetAdminDetailQueryHandler(IAdminRepository adminRepository, IMapper mapper)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
        }

        public async Task<AdminDetailDto> Handle(GetAdminDetailQuery request, CancellationToken cancellationToken)
        {
            var admin = await _adminRepository.GetByIdAsync(request.Id);
            return _mapper.Map<AdminDetailDto>(admin);
        }
    }
}

