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
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => CalculateAge(src.DateOfBirth)));

            CreateMap<Patient, PatientDetailDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => CalculateAge(src.DateOfBirth)))
                .ForMember(dest => dest.TotalAppointments, opt => opt.MapFrom(src => src.Appointments.Count))
                .ForMember(dest => dest.OutstandingBalance, opt => opt.MapFrom(src => src.GetTotalOutstandingBalance()));

            // Command to Entity mappings
            CreateMap<CreatePatientCommand, Patient>()
                .ConstructUsing(src => new Patient(
                    src.FirstName,
                    src.LastName,
                    src.Email,
                    src.PhoneNumber,
                    src.Address,
                    src.DateOfBirth,
                    src.IdentityUserId,
                    src.PatientIdNumber,
                    src.InsuranceProvider,
                    src.InsurancePolicyNumber));

            // DTO to Command mappings
            CreateMap<CreatePatientDto, CreatePatientCommand>();
            CreateMap<UpdatePatientDto, UpdatePatientCommand>();

            // Reverse mappings where needed
            CreateMap<PatientDto, Patient>().ReverseMap();
        }

        private static int CalculateAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}

