using MediatR;
using Microsoft.AspNetCore.Mvc;
using Clinic.Application.Features.Receptionist.Commands.CreateReceptionist;
using Clinic.Application.Features.Receptionist.Queries.GetReceptionistDetail;

namespace Clinic.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReceptionistsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReceptionistsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReceptionistDetailDto>> Get(int id)
        {
            var receptionist = await _mediator.Send(new GetReceptionistDetailQuery { Id = id });
            return Ok(receptionist);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] CreateReceptionistCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(id);
        }
    }
}

