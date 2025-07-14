using MediatR;
using Microsoft.AspNetCore.Mvc;
using Clinic.Application.Features.Pharmacist.Commands.CreatePharmacist;
using Clinic.Application.Features.Pharmacist.Queries.GetPharmacistDetail;

namespace Clinic.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PharmacistsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PharmacistsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PharmacistDetailDto>> Get(int id)
        {
            var pharmacist = await _mediator.Send(new GetPharmacistDetailQuery { Id = id });
            return Ok(pharmacist);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] CreatePharmacistCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(id);
        }
    }
}

