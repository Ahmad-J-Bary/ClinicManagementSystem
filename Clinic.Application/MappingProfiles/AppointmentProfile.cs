using AutoMapper;
using Clinic.Application.Features.Appointment.Commands.CreateAppointment;
using Clinic.Application.Features.Appointment.Commands.UpdateAppointment;
using Clinic.Application.Features.Appointment.Queries.GetAppointmentDetails;
using Clinic.Application.Features.Appointment.Queries.GetAppointments;
using Clinic.Domain.Entities;

namespace Clinic.Application.MappingProfiles;

public class AppointmentProfile : Profile
{
    public AppointmentProfile()
    {
        CreateMap<AppointmentDto, Appointment>().ReverseMap();
        CreateMap<Appointment, AppointmentDetailsDto>();
        CreateMap<CreateAppointmentCommand, Appointment>();
        CreateMap<UpdateAppointmentCommand, Appointment>();
    }
}

