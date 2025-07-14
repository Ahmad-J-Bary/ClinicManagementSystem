using MediatR;
using Microsoft.AspNetCore.Mvc;
using Clinic.Application.Features.Admin.Commands.CreateAdmin;
using Clinic.Application.Features.Admin.Commands.UpdateAdmin;
using Clinic.Application.Features.Admin.Commands.DeleteAdmin;
using Clinic.Application.Features.Admin.Queries.GetAdminDetail;

namespace Clinic.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AdminDetailDto>> Get(int id)
        {
            var admin = await _mediator.Send(new GetAdminDetailQuery { Id = id });
            return Ok(admin);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] CreateAdminCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] UpdateAdminCommand command)
        {
            command.Id = id;
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteAdminCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}

