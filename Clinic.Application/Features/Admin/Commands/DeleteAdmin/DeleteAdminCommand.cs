using MediatR;

namespace Clinic.Application.Features.Admin.Commands.DeleteAdmin
{
    public class DeleteAdminCommand : IRequest
    {
        public int Id { get; set; }
    }
}

