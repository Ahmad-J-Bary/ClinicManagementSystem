
using AutoMapper;
using Clinic.Application.Features.DoctorAvailability.Commands.CreateDoctorAvailability;
using Clinic.Application.Features.DoctorAvailability.Queries.GetDoctorAvailabilityDetail;
using Clinic.Domain.Entities;

namespace Clinic.Application.MappingProfiles
{
    public class DoctorAvailabilityProfile : Profile
    {
        public DoctorAvailabilityProfile()
        {
            CreateMap<DoctorAvailabilityDto, DoctorAvailability>().ReverseMap();
            CreateMap<DoctorAvailability, DoctorAvailabilityDetailDto>();
            CreateMap<CreateDoctorAvailabilityCommand, DoctorAvailability>();
        }
    }
}

