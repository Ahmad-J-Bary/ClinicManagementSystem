using MediatR;
using Microsoft.AspNetCore.Mvc;
using Clinic.Application.Features.Payment.Commands.CreatePayment;
using Clinic.Application.Features.Payment.Queries.GetPaymentDetail;

namespace Clinic.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDetailDto>> Get(int id)
        {
            var payment = await _mediator.Send(new GetPaymentDetailQuery { Id = id });
            return Ok(payment);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] CreatePaymentCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(id);
        }
    }
}

