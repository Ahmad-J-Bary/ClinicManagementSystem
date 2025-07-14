using MediatR;

namespace Clinic.Application.Features.Receptionist.Queries.GetReceptionistDetail
{
    public class GetReceptionistDetailQuery : IRequest<ReceptionistDetailDto>
    {
        public int Id { get; set; }
    }
}

