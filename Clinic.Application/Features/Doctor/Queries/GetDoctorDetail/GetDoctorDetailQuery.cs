using MediatR;

namespace Clinic.Application.Features.Doctor.Queries.GetDoctorDetail
{
    /// <summary>
    /// Query for retrieving detailed information about a specific doctor.
    /// Implements MediatR IRequest pattern for CQRS.
    /// </summary>
    public class GetDoctorDetailQuery : IRequest<DoctorDetailDto>
    {
        public int Id { get; set; }

        public GetDoctorDetailQuery(int id)
        {
            Id = id;
        }
    }
}

