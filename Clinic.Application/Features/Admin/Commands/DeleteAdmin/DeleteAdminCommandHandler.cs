
using MediatR;
using Clinic.Application.Contracts.Persistence;
using Clinic.Application.Exceptions;

namespace Clinic.Application.Features.Admin.Commands.DeleteAdmin
{
    public class DeleteAdminCommandHandler : IRequestHandler<DeleteAdminCommand>
    {
        private readonly IAdminRepository _adminRepository;

        public DeleteAdminCommandHandler(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<Unit> Handle(DeleteAdminCommand request, CancellationToken cancellationToken)
        {
            var adminToDelete = await _adminRepository.GetByIdAsync(request.Id);

            if (adminToDelete == null)
                throw new NotFoundException(nameof(Domain.Entities.Admin), request.Id);

            await _adminRepository.DeleteAsync(adminToDelete);
            return Unit.Value;
        }
    }
}

