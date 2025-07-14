using MediatR;

namespace Clinic.Application.Features.Pharmacist.Queries.GetPharmacistDetail
{
    public class GetPharmacistDetailQuery : IRequest<PharmacistDetailDto>
    {
        public int Id { get; set; }
    }
}

