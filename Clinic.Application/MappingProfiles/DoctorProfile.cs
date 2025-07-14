using AutoMapper;
using Clinic.Application.DTOs.Doctor;
using Clinic.Application.Features.Doctor.Commands.CreateDoctor;
using Clinic.Application.Features.Doctor.Commands.UpdateDoctor;
using Clinic.Application.Features.Doctor.Queries.GetDoctorDetail;
using Clinic.Domain.Entities;

namespace Clinic.Application.MappingProfiles
{
    /// <summary>
    /// AutoMapper profile for Doctor entity mappings.
    /// Defines mappings between Doctor domain entity and various DTOs, commands, and queries.
    /// </summary>
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {
            // Entity to DTO mappings
            CreateMap<Doctor, DoctorDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : string.Empty));

            CreateMap<Doctor, DoctorDetailDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : string.Empty));

            // Command to Entity mappings
            CreateMap<CreateDoctorCommand, Doctor>();

            // DTO to Command mappings
            CreateMap<CreateDoctorDto, CreateDoctorCommand>();
            CreateMap<UpdateDoctorDto, UpdateDoctorCommand>();

            // Reverse mappings where needed
            CreateMap<DoctorDto, Doctor>().ReverseMap();
        }
    }
}


