
using AutoMapper;
using Clinic.Application.Features.MedicalRecord.Commands.CreateMedicalRecord;
using Clinic.Application.Features.MedicalRecord.Queries.GetMedicalRecordDetail;
using Clinic.Domain.Entities;

namespace Clinic.Application.MappingProfiles
{
    public class MedicalRecordProfile : Profile
    {
        public MedicalRecordProfile()
        {
            CreateMap<MedicalRecordDto, MedicalRecord>().ReverseMap();
            CreateMap<MedicalRecord, MedicalRecordDetailDto>();
            CreateMap<CreateMedicalRecordCommand, MedicalRecord>();
        }
    }
}

