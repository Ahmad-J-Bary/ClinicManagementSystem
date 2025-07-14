using MediatR;
using Microsoft.AspNetCore.Mvc;
using Clinic.Application.Features.Notification.Commands.CreateNotification;

namespace Clinic.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NotificationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] CreateNotificationCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(id);
        }
    }
}

