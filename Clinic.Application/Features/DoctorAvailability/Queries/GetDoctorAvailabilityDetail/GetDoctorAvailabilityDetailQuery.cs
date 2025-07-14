using MediatR;

namespace Clinic.Application.Features.DoctorAvailability.Queries.GetDoctorAvailabilityDetail
{
    public class GetDoctorAvailabilityDetailQuery : IRequest<DoctorAvailabilityDetailDto>
    {
        public int Id { get; set; }
    }
}

