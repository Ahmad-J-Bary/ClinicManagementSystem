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
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : string.Empty))
                .ForMember(dest => dest.YearsOfExperience, opt => opt.MapFrom(src => CalculateYearsOfExperience(src.DateOfBirth)));

            CreateMap<Doctor, DoctorDetailDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : string.Empty))
                .ForMember(dest => dest.YearsOfExperience, opt => opt.MapFrom(src => CalculateYearsOfExperience(src.DateOfBirth)))
                .ForMember(dest => dest.TotalAppointments, opt => opt.MapFrom(src => src.Appointments.Count))
                .ForMember(dest => dest.UpcomingAppointments, opt => opt.MapFrom(src => src.GetUpcomingAppointments().Count()));

            // Command to Entity mappings
            CreateMap<CreateDoctorCommand, Doctor>()
                .ConstructUsing(src => new Doctor(
                    src.FirstName,
                    src.LastName,
                    src.Email,
                    src.PhoneNumber,
                    src.Address,
                    src.DateOfBirth,
                    src.IdentityUserId,
                    src.LicenseNumber,
                    src.Specialization,
                    src.DepartmentId,
                    src.Qualifications,
                    src.ConsultationFee));

            // DTO to Command mappings
            CreateMap<CreateDoctorDto, CreateDoctorCommand>();
            CreateMap<UpdateDoctorDto, UpdateDoctorCommand>();

            // Reverse mappings where needed
            CreateMap<DoctorDto, Doctor>().ReverseMap();
        }

        private static int CalculateYearsOfExperience(DateTime dateOfBirth)
        {
            // Assuming doctors start practicing around age 25-30
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > today.AddYears(-age)) age--;
            return Math.Max(0, age - 25); // Rough estimate of years of experience
        }
    }
}

