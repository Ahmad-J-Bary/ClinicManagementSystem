
using AutoMapper;
using MediatR;
using Clinic.Application.Contracts.Persistence;
using Clinic.Domain.Entities;

namespace Clinic.Application.Features.Notification.Commands.CreateNotification
{
    public class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommand, int>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;

        public CreateNotificationCommandHandler(INotificationRepository notificationRepository, IMapper mapper)
        {
            _notificationRepository = notificationRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = _mapper.Map<Domain.Entities.Notification>(request);
            notification = await _notificationRepository.AddAsync(notification);
            return notification.Id;
        }
    }
}

