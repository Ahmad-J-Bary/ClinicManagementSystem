
using AutoMapper;
using MediatR;
using Clinic.Application.Contracts.Persistence;

namespace Clinic.Application.Features.MedicalRecord.Queries.GetMedicalRecordDetail
{
    public class GetMedicalRecordDetailQueryHandler : IRequestHandler<GetMedicalRecordDetailQuery, MedicalRecordDetailDto>
    {
        private readonly IMedicalRecordRepository _medicalRecordRepository;
        private readonly IMapper _mapper;

        public GetMedicalRecordDetailQueryHandler(IMedicalRecordRepository medicalRecordRepository, IMapper mapper)
        {
            _medicalRecordRepository = medicalRecordRepository;
            _mapper = mapper;
        }

        public async Task<MedicalRecordDetailDto> Handle(GetMedicalRecordDetailQuery request, CancellationToken cancellationToken)
        {
            var medicalRecord = await _medicalRecordRepository.GetByIdAsync(request.Id);
            return _mapper.Map<MedicalRecordDetailDto>(medicalRecord);
        }
    }
}

