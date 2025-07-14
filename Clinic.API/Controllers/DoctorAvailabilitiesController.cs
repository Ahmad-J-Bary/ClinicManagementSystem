using MediatR;
using Microsoft.AspNetCore.Mvc;
using Clinic.Application.Features.DoctorAvailability.Commands.CreateDoctorAvailability;
using Clinic.Application.Features.DoctorAvailability.Queries.GetDoctorAvailabilityDetail;

namespace Clinic.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorAvailabilitiesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DoctorAvailabilitiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorAvailabilityDetailDto>> Get(int id)
        {
            var availability = await _mediator.Send(new GetDoctorAvailabilityDetailQuery { Id = id });
            return Ok(availability);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] CreateDoctorAvailabilityCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(id);
        }
    }
}

