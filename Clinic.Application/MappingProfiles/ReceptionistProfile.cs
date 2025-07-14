
using AutoMapper;
using Clinic.Application.Features.Receptionist.Commands.CreateReceptionist;
using Clinic.Application.Features.Receptionist.Queries.GetReceptionistDetail;
using Clinic.Domain.Entities;

namespace Clinic.Application.MappingProfiles
{
    public class ReceptionistProfile : Profile
    {
        public ReceptionistProfile()
        {
            CreateMap<ReceptionistDto, Receptionist>().ReverseMap();
            CreateMap<Receptionist, ReceptionistDetailDto>();
            CreateMap<CreateReceptionistCommand, Receptionist>();
        }
    }
}

