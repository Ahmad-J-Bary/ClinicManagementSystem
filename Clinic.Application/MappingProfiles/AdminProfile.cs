using AutoMapper;
using Clinic.Application.Features.Admin.Commands.CreateAdmin;
using Clinic.Application.Features.Admin.Queries.GetAdminDetail;
using Clinic.Domain.Entities;

namespace Clinic.Application.MappingProfiles
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<AdminDto, Admin>().ReverseMap();
            CreateMap<Admin, AdminDetailDto>();
            CreateMap<CreateAdminCommand, Admin>();
        }
    }
}

