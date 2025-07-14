
using AutoMapper;
using Clinic.Application.Features.Notification.Commands.CreateNotification;
using Clinic.Domain.Entities;

namespace Clinic.Application.MappingProfiles
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<NotificationDto, Notification>().ReverseMap();
            CreateMap<CreateNotificationCommand, Notification>();
        }
    }
}

