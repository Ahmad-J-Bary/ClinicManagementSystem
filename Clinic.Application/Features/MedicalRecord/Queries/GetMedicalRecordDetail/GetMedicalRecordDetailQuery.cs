using MediatR;

namespace Clinic.Application.Features.MedicalRecord.Queries.GetMedicalRecordDetail
{
    public class GetMedicalRecordDetailQuery : IRequest<MedicalRecordDetailDto>
    {
        public int Id { get; set; }
    }
}

