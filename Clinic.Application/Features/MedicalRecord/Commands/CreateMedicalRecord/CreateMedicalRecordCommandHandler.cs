
using AutoMapper;
using MediatR;
using Clinic.Application.Contracts.Persistence;
using Clinic.Domain.Entities;

namespace Clinic.Application.Features.MedicalRecord.Commands.CreateMedicalRecord
{
    public class CreateMedicalRecordCommandHandler : IRequestHandler<CreateMedicalRecordCommand, int>
    {
        private readonly IMedicalRecordRepository _medicalRecordRepository;
        private readonly IMapper _mapper;

        public CreateMedicalRecordCommandHandler(IMedicalRecordRepository medicalRecordRepository, IMapper mapper)
        {
            _medicalRecordRepository = medicalRecordRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateMedicalRecordCommand request, CancellationToken cancellationToken)
        {
            var medicalRecord = _mapper.Map<Domain.Entities.MedicalRecord>(request);
            medicalRecord = await _medicalRecordRepository.AddAsync(medicalRecord);
            return medicalRecord.Id;
        }
    }
}

