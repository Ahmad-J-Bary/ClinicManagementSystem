using MediatR;

namespace Clinic.Application.Features.Patient.Queries.GetPatientDetail
{
    /// <summary>
    /// Query for retrieving detailed information about a specific patient.
    /// Implements MediatR IRequest pattern for CQRS.
    /// </summary>
    public class GetPatientDetailQuery : IRequest<PatientDetailDto>
    {
        public int Id { get; set; }

        public GetPatientDetailQuery(int id)
        {
            Id = id;
        }
    }
}

