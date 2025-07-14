using AutoMapper;
using Clinic.Application.DTOs.Patient;
using Clinic.Application.Features.Patient.Commands.CreatePatient;
using Clinic.Application.Features.Patient.Commands.UpdatePatient;
using Clinic.Application.Features.Patient.Queries.GetPatientDetail;
using Clinic.Domain.Entities;

namespace Clinic.Application.MappingProfiles
{
    /// <summary>
    /// AutoMapper profile for Patient entity mappings.
    /// Defines mappings between Patient domain entity and various DTOs, commands, and queries.
    /// </summary>
    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            // Entity to DTO mappings
            CreateMap<Patient, PatientDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            CreateMap<Patient, PatientDetailDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            // Command to Entity mappings
            CreateMap<CreatePatientCommand, Patient>();

            // DTO to Command mappings
            CreateMap<CreatePatientDto, CreatePatientCommand>();
            CreateMap<UpdatePatientDto, UpdatePatientCommand>();

            // Reverse mappings where needed
            CreateMap<PatientDto, Patient>().ReverseMap();
        }
    }
}


