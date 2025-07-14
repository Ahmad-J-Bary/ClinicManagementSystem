using MediatR;

namespace Clinic.Application.Features.Admin.Queries.GetAdminDetail
{
    public class GetAdminDetailQuery : IRequest<AdminDetailDto>
    {
        public int Id { get; set; }
    }
}

